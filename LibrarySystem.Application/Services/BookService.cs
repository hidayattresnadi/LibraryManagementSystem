using LibrarySystem.Application.QueryParameter;
using LibrarySystem.Domain.Models;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Application.DTO;

namespace LibrarySystem.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<BookDTO> AddBook(BookDTO inputBook)
        {
            var book = await _bookRepository.GetFirstOrDefaultAsync(book => book.ISBN == inputBook.ISBN);
            if (book != null) 
            {
                throw new BadRequestException ("ISBN is already exist in another book, data cannot be dupplicate");
            }
            var newBook = new Book
            {
                Title = inputBook.Title,
                Category = inputBook.Category,
                Author = inputBook.Author,
                PublicationYear = inputBook.PublicationYear,
                Publisher = inputBook.Publisher,
                Description = inputBook.Description,
                Price = inputBook.Price,
                PurchaseDate = inputBook.PurchaseDate,
                ISBN = inputBook.ISBN,
                LibraryLocation = inputBook.LibraryLocation
            };
            await _bookRepository.AddAsync(newBook);
            await _bookRepository.SaveAsync();
            return inputBook;
        }
        public async Task<IEnumerable<BookDTOGetAll>> GetAllBooks(QueryParameterBook queryParameterBook)
        {
            var filteredBooks = await _bookRepository.GetAllAsyncBookFiltered2(queryParameterBook);
            var bookDTOs = filteredBooks.Select(book => new BookDTOGetAll
            {
                ISBN = book.ISBN,
                Category = book.Category,
                Author = book.Author,
                Title = book.Title
            });
            return bookDTOs;
        }
         public async Task<IEnumerable<BookDTOGetAll>> GetAllBooks2(QueryParameterBook2 queryParameterBook)
        {
            var filteredBooks = await _bookRepository.GetAllAsyncBookFiltered(queryParameterBook);
            var bookDTOs = filteredBooks.Select(book => new BookDTOGetAll
            {
                ISBN = book.ISBN,
                Category = book.Category,
                Author = book.Author,
                Title = book.Title
            });
            return bookDTOs;
        }
        public async Task<Book> GetBookById(int id)
        {
            Book chosenBook = await _bookRepository.GetFirstOrDefaultAsync(foundBook => foundBook.Id == id);
            if (chosenBook == null){
                throw new NotFoundException("Book is not found");
            }
            return chosenBook;
        }
        public async Task<BookDTOGetDetail> GetBookDetail(int id)
        {
            Book chosenBook = await GetBookById(id);
            var bookDTODetail = new BookDTOGetDetail
            {
                Category = chosenBook.Category,
                Title = chosenBook.Title,
                ISBN = chosenBook.ISBN,
                Publisher = chosenBook.Publisher,
                Description = chosenBook.Description,
                LibraryLocation = chosenBook.LibraryLocation,
            };
            return bookDTODetail;
        }
        public async Task<BookDTO> UpdateBook(BookDTO book, int id)
        {
            var foundBook = await GetBookById(id);
            var findingIsbnBook = await _bookRepository.GetFirstOrDefaultAsync(b => b.ISBN == book.ISBN);
            if (findingIsbnBook != null && findingIsbnBook.Id != id)
            {
                throw new BadRequestException ("ISBN is already exist in another book, data cannot be dupplicate");
            }
            var mappingBook = new Book
            {
                Title = book.Title,
                Category = book.Category,
                Author = book.Author,
                PublicationYear = book.PublicationYear,
                Publisher = book.Publisher,
                Description = book.Description,
                Price = book.Price,
                PurchaseDate = book.PurchaseDate,
                ISBN = book.ISBN,
                LibraryLocation = book.LibraryLocation
            };
            var updatedBook = _bookRepository.Update(foundBook,mappingBook);
            await _bookRepository.SaveAsync();
            return book;
        }
        public async Task<bool> DeleteBook(int id)
        {
            var foundBook = await GetBookById(id);
            _bookRepository.Remove(foundBook);
            await _bookRepository.SaveAsync();
            return true;
        }
        public async Task<Book> UpdateIntoDeletedBook(int id, string deleteReasoning)
        {
            var foundBook = await GetBookById(id);
            _bookRepository.UpdateIntoDeletedBook(foundBook,deleteReasoning);
            await _bookRepository.SaveAsync();
            return foundBook;
        }
    }
}
