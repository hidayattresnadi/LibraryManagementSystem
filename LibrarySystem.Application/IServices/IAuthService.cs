using LibrarySystem.Application.Services;
using LibrarySystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.IServices
{
    public  interface IAuthService
    {
        Task<Response> Register(Register registerData);
        Task<Response> Login(Login loginData);
        Task<Response> CreateRoleAsync(string rolename);
        Task<Response> RefreshToken(RefreshTokenRequest request);
        Task<Response> LogoutAsync(string email);

    }
}
