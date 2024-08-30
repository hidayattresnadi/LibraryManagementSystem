using LibrarySystem.Application.DTO.UserDTO;
using LibrarySystem.Application.IServices;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Application.Roles;
using LibrarySystem.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        public AuthService(UserManager<AppUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager, IUserService userService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _userService = userService;
        }
        public async Task<Response> Register(Register model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);

            if (userExists != null)

                return new Response { Status = "Error", Message = "User already exists!" };

            AppUser user = new AppUser()

            {
                Email = model.Email,

                SecurityStamp = Guid.NewGuid().ToString(),

                UserName = model.UserName
            };

            UserDTO newUser = new UserDTO
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Position = model.Position,
                Privilege = model.Privilege
            };

            await _userService.AddUser(newUser);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)

                return new Response
                {
                    Status = "Error",
                    Message = "User creation failed! Please check user details and try again."
                };

            if (!string.IsNullOrEmpty(model.Role) && await _roleManager.RoleExistsAsync(model.Role))
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, Roles.Roles.Role_Library_User);
            }

            return new Response
            {
                Status = "Success",
                Message = "User created successfully!"
            };
        }
        public async Task<Response> Login(Login loginData)
        {
            var user = await _userManager.FindByEmailAsync(loginData.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginData.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));

                var token = new JwtSecurityToken(

                issuer: _configuration["JWT:Issuer"],

                audience: _configuration["JWT:Audience"],

                expires: DateTime.Now.AddHours(3),

                claims: authClaims,

                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)

                );
                // Generate a new refresh token.
                var refreshToken = GenerateRefreshToken();
                // Save the new refresh token with associated user information.
                user.RefreshToken = refreshToken.Token;
                user.RefreshTokenExpire = refreshToken.ExpiryDate;
                await _userManager.UpdateAsync(user);
                return new AuthLoginResponse
                {
                    Message = "Login Success",
                    Status = "Success",
                    ExpiredOn = token.ValidTo,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken.Token,
                    RefreshTokenExpireOn = refreshToken.ExpiryDate
                };
            }
            return new Response { Status = "Error", Message = "Password not valid!" };
        }
        public async Task<Response> CreateRoleAsync(string rolename)
        {
            if (!await _roleManager.RoleExistsAsync(rolename))

                await _roleManager.CreateAsync(new IdentityRole(rolename));

            return new Response { Status = "Success", Message = "Role created successfully!" };
        }
        public async Task<Response> RefreshToken(RefreshTokenRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.RefreshToken))
            {
                return new Response { Status = "Error", Message = "Invalid Request" };
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);
            if (user == null)
            {
                return new Response { Status = "Error", Message = "Invalid Token" };
            }
            if (user.RefreshTokenExpire < DateTime.UtcNow)
            {
                user.RefreshTokenExpire = null;
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
                return new Response { Status = "Error", Message = "Refresh token expired. Please log in again." };
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName), // Nama pengguna
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // ID unik untuk token
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            // Generate new access token
            var newAccessToken = GenerateAccessToken(authClaims);

            return new AuthLoginResponse
            {
                Token = newAccessToken,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpireOn = user.RefreshTokenExpire.Value
            };
        }
        public async Task<Response> LogoutAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpire = null;
                await _userManager.UpdateAsync(user);
            }
            return new Response
            {
                Message = "Success Logout",
                Status = "Success",
            };
        }
         // Generates a new refresh token using a cryptographic random number generator.
        private static RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];  // Prepare a buffer to hold the random bytes.
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);  // Fill the buffer with cryptographically strong random bytes.

                var token = Convert.ToBase64String(randomNumber); // Convert the bytes to a Base64 string and return.
                return new RefreshToken
                {
                    Token = token,
                    ExpiryDate = DateTime.UtcNow.AddDays(7)
                };
            }
        }
        private string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddHours(3), // Set waktu kadaluarsa yang sesuai
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
