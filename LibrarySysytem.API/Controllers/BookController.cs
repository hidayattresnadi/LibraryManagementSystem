using LibrarySystem.Application.DTO;
using LibrarySystem.Application.IServices;
using LibrarySystem.Application.Mail;
using LibrarySystem.Application.QueryParameter;
using LibrarySystem.Application.Roles;
using LibrarySystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LibrarySysytem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IEmailService _emailService;
        public BookController(IBookService bookService,IEmailService emailService)
        {
            _bookService = bookService;
            _emailService = emailService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookDTO book)
        {
            var inputBook = await _bookService.AddBook(book);
            return Ok(inputBook);
        }
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllBooks([FromQuery] string? title,
                                                     [FromQuery] string? logicOperator,
                                                     [FromQuery] string? author,
                                                     [FromQuery] string? category,
                                                     [FromQuery] string? isbn)
        {
            var queryParameters = new QueryParameterBook
            {
                ISBN = isbn,
                Category = category,
                Title = title,
                Author = author,
                LogicOperator =logicOperator,
            };
            var books = await _bookService.GetAllBooks(queryParameters);
            var htmlTemplate = System.IO.File.ReadAllText(@"./Templates/EmailTemplate/Whatever.html");
            var bookDetailsHtml = new StringBuilder();
            foreach (var book in books)
            {
                bookDetailsHtml.AppendLine($"<li>Title: {book.Title}, Author: {book.Author}, ISBN: {book.ISBN}</li>");
            }
            htmlTemplate = htmlTemplate.Replace("{{Name}}", "Traveler");
            htmlTemplate = htmlTemplate.Replace("{{BookDetails}}", bookDetailsHtml.ToString());
            var mailData = new MailData
            {
                EmailToName = "Traveler",
                EmailSubject = "Books Query Results",
                EmailBody = $"Found {books.Count()} books based on your query."
            };
            mailData.EmailToIds.Add("hidayattresnadi@gmail.com");
            mailData.EmailToIds.Add("hidayat.tresnadi@solecode.id");
            mailData.EmailCCIds.Add("kallenkaslana243@gmail.com");
            mailData.EmailBody = htmlTemplate;

            // Send email
            var emailResult = _emailService.SendEmailAsync(mailData);

            if (!emailResult)
            {
                return StatusCode(500, "Error sending email.");
            }

            return Ok(books);
        }
        [Authorize]
        [HttpGet("V2")]
        public async Task<IActionResult> GetAllBooks2([FromQuery] string? title,
                                                     [FromQuery] string? logicOperator1,
                                                     [FromQuery] string? author,
                                                     [FromQuery] string? logicOperator2,
                                                     [FromQuery] string? category,
                                                     [FromQuery] string? logicOperator3,
                                                     [FromQuery] string? isbn,
                                                     [FromQuery] string? language,
                                                     [FromQuery] int pageNumber,
                                                     [FromQuery] int pageSize
            )
        {
            var queryParameters = new QueryParameterBook2
            {
                ISBN = isbn,
                Category = category,
                Title = title,
                Author = author,
                LogicOperator1 =logicOperator1,
                LogicOperator2 =logicOperator2,
                LogicOperator3 =logicOperator3,
                Language = language,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var books = await _bookService.GetAllBooks2(queryParameters);
            return Ok(books);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            BookDTOGetDetail book = await _bookService.GetBookDetail(id);
            return Ok(book);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditBook([FromBody] BookDTO book, int id)
        {
            var updatedBook = await _bookService.UpdateBook(book, id);
            return Ok(updatedBook);
        }
        [Authorize(Roles =Roles.Role_Librarian+","+Roles.Role_Library_Manager)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBook(id);
            return Ok("Book is deleted");
        }
        [Authorize(Roles = Roles.Role_Library_User)]
        [HttpDelete("book_deleted/{id}")]
        public async Task<IActionResult> UpdateIntoDeletedBook (int id, [FromBody] string deleteReasoning)
        {
            var book = await _bookService.UpdateIntoDeletedBook(id,deleteReasoning);
            return Ok(book);
        }
        [Authorize(Roles =Roles.Role_Library_User)]
        [HttpPost("book_request/{id}")]
        public async Task<IActionResult> AddRequestAddingBook(BookDTORequest request, int id)
        {
            var isAddingRequest = await _bookService.AddRequestAddingBook(request, id);
            if (isAddingRequest != true) 
            {
                return BadRequest("Adding book request failed");
            }
            return Ok("Adding book request success");
        }
    }
}
