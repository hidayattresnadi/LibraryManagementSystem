using LibrarySystem.Application.DTO;
using LibrarySystem.Application.QueryParameter;
using LibrarySystem.Application.Roles;
using LibrarySystem.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySysytem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookDTO book)
        {
            var inputBook = await _bookService.AddBook(book);
            return Ok(inputBook);
        }
        [Authorize]
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
    }
}
