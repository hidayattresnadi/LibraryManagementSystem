using LibrarySystem.Application.DTO.Borrowing;
using LibrarySystem.Application.DTO.UserDTO;
using LibrarySystem.Application.IServices;
using LibrarySystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySysytem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowingController : ControllerBase
    {
        private readonly IBorrowingService _borrowingService;
        public BorrowingController(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }
        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout([FromBody] UserCheckout userCheckout)
        {
            if (!ModelState.IsValid)

                return BadRequest(ModelState);

            var result = await _borrowingService.CheckOutBooks(userCheckout);

            if (result.Status == "Error")

                return BadRequest(result.Message);

            return Ok(result);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBorrowsByUserId(int userId)
        {
            var borrows = await _borrowingService.GetBorrowsByUserId(userId);
            return Ok(borrows);
        }
        [HttpGet("books/search_criteria")]
        public async Task<IActionResult> GetSignOutBooks([FromQuery] SearchCriteria searchCriteria)
        {
            var borrows = await _borrowingService.GenerateBorrowedBookAsync(searchCriteria);
            return Ok(borrows);
        }
        [HttpGet("users_late_return")]
        public async Task<IActionResult> GetUserLateReturn()
        {
            var borrows = await _borrowingService.GenerateOverdueBorrowingUsersAsync();
            return Ok(borrows);
        }
        [HttpGet("report/late_users")]
        public async Task<IActionResult> Report()
        {

            var Filename = "BorrowingReport.pdf";

            var file = await _borrowingService.GenerateOverdueBorrowingUsersPdfAsync();

            return File(file, "application/pdf", Filename);

        }
        [HttpGet("report/books/search_criteria")]
        public async Task<IActionResult> ReportBySearchCriteria([FromQuery] SearchCriteria searchCriteria)
        {

            var Filename = "BorrowingReport.pdf";

            var file = await _borrowingService.GenerateBorrowedBookReportAsync(searchCriteria);

            return File(file, "application/pdf", Filename);

        }

        [HttpGet("report/user/{id}")]
        public async Task<IActionResult> ReportByUserId(int id)
        {

            var Filename = "BorrowingReport.pdf";

            var file = await _borrowingService.GenerateUserBorrowedReportPdfAsync(id);

            return File(file, "application/pdf", Filename);

        }
    }
}
