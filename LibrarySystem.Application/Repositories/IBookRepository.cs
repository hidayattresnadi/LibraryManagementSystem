using LibrarySystem.Domain.Models;
using LibrarySystem.Application.QueryParameter;

namespace LibrarySystem.Application.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Book Update(Book foundBook, Book book);
        Book UpdateIntoDeletedBook(Book foundBook, string deleteReasoning);
        Task<IEnumerable<Book>> GetAllAsyncBookFiltered2(QueryParameterBook queryParameterBook);
        Task<IEnumerable<Book>> GetAllAsyncBookFiltered(QueryParameterBook2 queryParameterBook);
    }
}
