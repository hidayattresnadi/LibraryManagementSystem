using LibrarySystem.Domain.Models;
using LibrarySystem.Application.DTO;
using LibrarySystem.Application.QueryParameter;

namespace LibrarySystem.Application.Services
{
    public interface IBookService
    {
        Task<BookDTO> AddBook(BookDTO  book);
        Task<IEnumerable<BookDTOGetAll>> GetAllBooks(QueryParameterBook queryParameterBook);
        Task<IEnumerable<BookDTOGetAll>> GetAllBooks2(QueryParameterBook2 queryParameterBook);
        Task<Book> GetBookById(int id);
        Task<BookDTOGetDetail> GetBookDetail(int id);
        Task<BookDTO> UpdateBook(BookDTO book, int id);
        Task<bool> DeleteBook(int id);
        Task<Book> UpdateIntoDeletedBook(int id, string deleteReasoning);
    }
}
