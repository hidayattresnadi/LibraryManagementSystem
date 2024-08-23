using LibrarySystem.Application.QueryParameter;
using LibrarySystem.Domain.Models;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly MyDbContext _db;
        public BookRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }

        public Book Update(Book foundBook, Book book)
        {
            foundBook.Title = book.Title;
            foundBook.Category = book.Category;
            foundBook.Author = book.Author;
            foundBook.PublicationYear = book.PublicationYear;
            foundBook.Publisher = book.Publisher;
            foundBook.Description = book.Description;
            foundBook.Price = book.Price;
            foundBook.PurchaseDate = book.PurchaseDate;
            foundBook.ISBN = book.ISBN;
            foundBook.LibraryLocation = book.LibraryLocation;
            return foundBook;
        }
        public Book UpdateIntoDeletedBook(Book foundBook, string deleteReasoning)
        {
            foundBook.DeleteStamp = true;
            foundBook.DeleteReasoning = deleteReasoning;
            return foundBook;
        }
        public async Task<IEnumerable<Book>> GetAllAsyncBookFiltered(QueryParameterBook2 queryParameterBook)
        {
            var query = _db.Books.AsQueryable();
            if (queryParameterBook.LogicOperator3 == null)
            {
                if (queryParameterBook.LogicOperator1?.ToUpper() == "OR")
                {
                    if (queryParameterBook.LogicOperator2 == null)
                    {
                        query = query.Where(b =>
                        !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) ||
                        !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()))
                    ;
                    }
                    else if (queryParameterBook.LogicOperator2?.ToUpper() == "AND")
                    {
                        query = query.Where(b =>
                       !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) ||
                       !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()) &&
                       !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()))
                   ;
                    }

                }
                else if (queryParameterBook.LogicOperator1?.ToUpper() == "AND" && queryParameterBook.LogicOperator2 == null)
                {
                    if (!string.IsNullOrEmpty(queryParameterBook.Title))
                    {
                        query = query.Where(b => b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()));
                    }
                    if (!string.IsNullOrEmpty(queryParameterBook.Author))
                    {
                        query = query.Where(b => b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()));
                    }
                }
                if (queryParameterBook.LogicOperator2?.ToUpper() == "OR")
                {
                    if (queryParameterBook.LogicOperator1 == null)
                        query = query.Where(b =>
                            !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) ||
                            !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()))
                        ;
                    else if (queryParameterBook.LogicOperator1?.ToUpper() == "OR")
                    {
                        query = query.Where(b =>
                        !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()) ||
                        !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) ||
                        !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()))
                    ;
                    }
                    else if (queryParameterBook.LogicOperator1?.ToUpper() == "AND")
                    {
                        query = query.Where(b =>
                       !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()) ||
                       !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) &&
                       !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()))
                   ;
                    }
                }
                else if (queryParameterBook.LogicOperator2?.ToUpper() == "AND")
                {
                    if (queryParameterBook.LogicOperator1?.ToUpper() == "AND")
                    {
                        if (!string.IsNullOrEmpty(queryParameterBook.Title))
                        {
                            query = query.Where(b => b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(queryParameterBook.Author))
                        {
                            query = query.Where(b => b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()));
                        }
                        if (!string.IsNullOrEmpty(queryParameterBook.Category))
                        {
                            query = query.Where(b => b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()));
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(queryParameterBook.Title))
                    {
                        query = query.Where(b => b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()));
                    }
                    if (!string.IsNullOrEmpty(queryParameterBook.Author))
                    {
                        query = query.Where(b => b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()));
                    }
                    if (!string.IsNullOrEmpty(queryParameterBook.Category))
                    {
                        query = query.Where(b => b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()));
                    }
                    if (!string.IsNullOrEmpty(queryParameterBook.ISBN))
                    {
                        query = query.Where(b => b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()));
                    }
                }
            }
            else if (queryParameterBook.LogicOperator3?.ToUpper() == "OR")
            {
                if (queryParameterBook.LogicOperator1 == null && queryParameterBook.LogicOperator2 == null)
                {
                    query = query.Where(b =>
                           !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()));
                }
                else if (queryParameterBook.LogicOperator1 == null && queryParameterBook.LogicOperator2?.ToUpper() == "OR")
                {
                    query = query.Where(b =>
                           !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()));
                }
                else if (queryParameterBook.LogicOperator1 == null && queryParameterBook.LogicOperator2?.ToUpper() == "AND")
                {
                    query = query.Where(b =>
                           !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) &&
                           !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()))
                           ;
                }
                else if (queryParameterBook.LogicOperator1?.ToUpper() == "OR" && queryParameterBook.LogicOperator2 == null)
                {
                    query = query.Where(b =>
                           !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()))
                           ;
                }
                else if (queryParameterBook.LogicOperator1?.ToUpper() == "AND" && queryParameterBook.LogicOperator2 == null)
                {
                    query = query.Where(b =>
                           !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) &&
                           !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()))
                           ;
                }
                else if (queryParameterBook.LogicOperator1?.ToUpper() == "OR" && queryParameterBook.LogicOperator2?.ToUpper() == "OR")
                {
                    query = query.Where(b =>
                           !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()))
                           ;
                }
                else if (queryParameterBook.LogicOperator1?.ToUpper() == "AND" && queryParameterBook.LogicOperator2?.ToUpper() == "OR")
                {
                    query = query.Where(b =>
                           !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()) &&
                           !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()))
                           ;
                }
                else if (queryParameterBook.LogicOperator1?.ToUpper() == "AND" && queryParameterBook.LogicOperator2?.ToUpper() == "AND")
                {
                    query = query.Where(b =>
                           !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()) &&
                           !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) &&
                           !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()))
                           ;
                }
                else if (queryParameterBook.LogicOperator1?.ToUpper() == "OR" && queryParameterBook.LogicOperator2?.ToUpper() == "AND")
                {
                    query = query.Where(b =>
                           !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) &&
                           !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()) ||
                           !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()))
                           ;
                }
            }
            else if (queryParameterBook.LogicOperator3?.ToUpper() == "AND")
            {
                if (queryParameterBook.LogicOperator1 == null && queryParameterBook.LogicOperator2 == null)
                {
                    query = query.Where(b =>
                           !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()) &&
                           !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()));
                }
                else if (queryParameterBook.LogicOperator1?.ToUpper() == "OR" && queryParameterBook.LogicOperator2 == null)
                {
                    query = query.Where(b =>
                          !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) ||
                          !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()) &&
                          !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()));
                }
                else if (queryParameterBook.LogicOperator1?.ToUpper() == "AND" && queryParameterBook.LogicOperator2 == null)
                {
                    query = query.Where(b =>
                          !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) &&
                          !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()) &&
                          !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()));
                }
                else if (queryParameterBook.LogicOperator1 == null && queryParameterBook.LogicOperator2?.ToUpper() == "AND")
                {
                    query = query.Where(b =>
                          !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) &&
                          !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()) &&
                          !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()));
                }
                else if (queryParameterBook.LogicOperator1 == null && queryParameterBook.LogicOperator2?.ToUpper() == "OR")
                {
                    query = query.Where(b =>
                          !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) ||
                          !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()) &&
                          !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()));
                }
                else if (queryParameterBook.LogicOperator1?.ToUpper() == "AND" && queryParameterBook.LogicOperator2?.ToUpper() == "AND")
                {
                    query = query.Where(b =>
                          !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) &&
                          !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()) &&
                          !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()) &&
                          !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()));
                }
                else if (queryParameterBook.LogicOperator1?.ToUpper() == "OR" && queryParameterBook.LogicOperator2?.ToUpper() == "AND")
                {
                    query = query.Where(b =>
                          !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) ||
                          !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()) &&
                          !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()) &&
                          !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()));
                }
                else if (queryParameterBook.LogicOperator1?.ToUpper() == "AND" && queryParameterBook.LogicOperator2?.ToUpper() == "OR")
                {
                    query = query.Where(b =>
                          !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) &&
                          !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()) ||
                          !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()) &&
                          !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()));
                }
                else if (queryParameterBook.LogicOperator1?.ToUpper() == "OR" && queryParameterBook.LogicOperator2?.ToUpper() == "OR")
                {
                    query = query.Where(b =>
                          !string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()) ||
                          !string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()) ||
                          !string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()) &&
                          !string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()));
                }
            }
            if (!string.IsNullOrEmpty(queryParameterBook.Language))
            {
                query = query.Where(b => b.Language.ToLower().Contains(queryParameterBook.Language.ToLower()));
            }
            query = query.OrderBy(b => b.Title).Skip((queryParameterBook.PageNumber - 1) * queryParameterBook.PageSize).Take(queryParameterBook.PageSize);
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetAllAsyncBookFiltered2(QueryParameterBook queryParameterBook)
        {
            var query = _db.Books.AsQueryable();
            if (queryParameterBook.LogicOperator?.ToUpper() == "OR")
            {
                query = query.Where(b =>
                    (!string.IsNullOrEmpty(queryParameterBook.ISBN) && b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower())) ||
                    (!string.IsNullOrEmpty(queryParameterBook.Author) && b.Author.ToLower().Contains(queryParameterBook.Author.ToLower())) ||
                    (!string.IsNullOrEmpty(queryParameterBook.Title) && b.Title.ToLower().Contains(queryParameterBook.Title.ToLower())) ||
                    (!string.IsNullOrEmpty(queryParameterBook.Category) && b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()))
                );
            }
            else
            {
                if (!string.IsNullOrEmpty(queryParameterBook.ISBN))
                {
                    query = query.Where(b => b.ISBN.ToLower().Contains(queryParameterBook.ISBN.ToLower()));
                }
                if (!string.IsNullOrEmpty(queryParameterBook.Author))
                {
                    query = query.Where(b => b.Author.ToLower().Contains(queryParameterBook.Author.ToLower()));
                }
                if (!string.IsNullOrEmpty(queryParameterBook.Title))
                {
                    query = query.Where(b => b.Title.ToLower().Contains(queryParameterBook.Title.ToLower()));
                }
                if (!string.IsNullOrEmpty(queryParameterBook.Category))
                {
                    query = query.Where(b => b.Category.ToLower().Contains(queryParameterBook.Category.ToLower()));
                }
            }
            query = query.OrderBy(b => b.Title);
            return await query.ToListAsync();
        }
    }
}