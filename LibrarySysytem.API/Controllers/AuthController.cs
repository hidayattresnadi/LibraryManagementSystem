using LibrarySystem.Application.IServices;
using LibrarySystem.Application.Mail;
using LibrarySystem.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibrarySysytem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        public AuthController(IAuthService authService,IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }
        [HttpPost("register")]

        public async Task<IActionResult> RegisterAsync([FromForm] IFormFileCollection Attachments, [FromForm] Register model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Register(model);
            var htmlTemplate = System.IO.File.ReadAllText(@"./Templates/EmailTemplate/RegisterPage.html");
            htmlTemplate = htmlTemplate.Replace("{{Name}}", model.UserName);
            htmlTemplate = htmlTemplate.Replace("{{Password}}", model.Password);
            htmlTemplate = htmlTemplate.Replace("{{Email}}", model.Email);

            var mailData = new MailData
            {
                EmailToName = model.UserName,
                EmailSubject = "Books Query Results",
                Attachments = Attachments,
            };
            //mailData.EmailToIds.Add("hidayattresnadi@gmail.com");
            //mailData.EmailToIds.Add("hidayat.tresnadi@solecode.id");
            //mailData.EmailCCIds.Add("kallenkaslana243@gmail.com");
            mailData.EmailBody = htmlTemplate;

            // Send email
            var emailResult = _emailService.SendEmailAsync(mailData);
            if (result.Status == "Error")
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPost("login")]

        public async Task<IActionResult> LoginAsync([FromBody] Login model)
        {
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            var result = await _authService.Login(model);

            if (result.Status == "Error")

                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpPost("role")]
        public async Task<IActionResult> CreateRoleAsync([FromBody]string rolename)
        {
            var result = await _authService.CreateRoleAsync(rolename);
            return Ok(result);

        }
        [HttpPost("refresh-Token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequest request)
        {
            var result = await _authService.RefreshToken(request);
            return Ok(result);
        }
        [HttpPatch("Log-Out")]
        public async Task<IActionResult> LogoutAsync([FromBody]string email)
        {
            var response = await _authService.LogoutAsync(email);
            return Ok(response);
        }
        [HttpPatch("/assign_role/{userId}")]
        public async Task<IActionResult> AssignRoleAsync(string userId, [FromBody] string roleName)
        {
            var result = await _authService.AssignRoleAsync(userId, roleName);

            if (result.Status == "Error")
                return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
