using LibrarySystem.Application.DTO;
using LibrarySystem.Application.IServices;
using LibrarySystem.Application.Roles;
using LibrarySystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySysytem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly IProcessService _processService;
        private readonly IEmailService _emailService;
        public ProcessController(IProcessService processService, IEmailService emailService)
        {
            _processService = processService;
            _emailService = emailService;
        }
        [Authorize(Roles = Roles.Role_Library_Manager + "," + Roles.Role_Librarian)]
        [HttpPost("{id}")]
        public async Task<IActionResult> ReviewRequestBookAdding([FromBody] RequestApproval request,int id)
        {
            var result = await _processService.ReviewRequest(id,request);

            if (result.Status == "Error")

                return BadRequest(result.Message);

            return Ok(result);
        }

    }
}
