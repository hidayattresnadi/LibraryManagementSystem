using LibrarySystem.Application.QueryParameter;
using LibrarySystem.Domain.Models;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Application.DTO;
using LibrarySystem.Domain.IRepositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using LibrarySystem.Application.Mail;
using System.Net.Mail;
using LibrarySystem.Application.IServices;
using Microsoft.AspNetCore.Identity;
using LibrarySystem.Application.Roles;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using LibrarySystem.Domain.DTO.Dashboard;
using System.Collections.Generic;

namespace LibrarySystem.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IWorkflowActionRepository _workflowActionRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly IProcessRepository _processRepository;
        private readonly IWorkflowSequenceRepository _workflowSequenceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INextStepRulesRepository _nextStepRulesRepository;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IBookRequestRepository _bookRequestRepository;

        public BookService(IBookRepository bookRepository, IRequestRepository requestRepository, IProcessRepository processRepository, 
                           IWorkflowActionRepository workflowActionRepository, IHttpContextAccessor httpContextAccessor, IWorkflowSequenceRepository workflowSequenceRepository,
                           INextStepRulesRepository nextStepRulesRepository, IEmailService emailService, UserManager<AppUser> userManager,
                           RoleManager<IdentityRole> roleManager,IBookRequestRepository bookRequestRepository)
        {
            _bookRepository = bookRepository;
            _requestRepository = requestRepository;
            _processRepository = processRepository;
            _workflowActionRepository = workflowActionRepository;
            _httpContextAccessor = httpContextAccessor;
            _workflowSequenceRepository = workflowSequenceRepository;
            _nextStepRulesRepository = nextStepRulesRepository;
            _emailService = emailService;
            _userManager = userManager;
            _roleManager = roleManager;
            _bookRequestRepository = bookRequestRepository;
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
        public async Task<bool> AddRequestAddingBook (BookDTORequest request, int workflowId)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var newRequest = new Request { 
                ProcessName = "Book Adding Request",
                Description = "Process for handling book adding request",
                StartDate = DateTime.UtcNow
            };
            await _requestRepository.AddAsync(newRequest);
            await _requestRepository.SaveAsync();

            var currentStepId = await _workflowSequenceRepository.GetFirstOrDefaultAsync(wfs => wfs.WorkflowId == workflowId);
            var nextStepId = await _nextStepRulesRepository.GetFirstOrDefaultAsync(n => n.CurrentStepId == currentStepId.StepId);

            var newProcess = new Process
            {
                RequesterId = userId,
                WorkflowId = workflowId,
                RequestType = "Adding Book",
                Status = "Pending",
                RequestDate = DateTime.UtcNow,
                RequestId = newRequest.RequestId,
                CurrentStepId = nextStepId.NextStepId
            };
            await _processRepository.AddAsync(newProcess);
            await _processRepository.SaveAsync();

            var newBookRequest = new BookRequest
            {
                ProcessId = newProcess.ProcessId,
                Title = request.Title,
                ISBN= request.ISBN,
                Author=request.Author,
                Publisher=request.Publisher
            };

            await _bookRequestRepository.AddAsync(newBookRequest);
            await _bookRequestRepository.SaveAsync();

            var newWorkflowAction = new WorkflowAction
            {
               ProcessId = newProcess.ProcessId,
               StepId = nextStepId.CurrentStepId,
               ActorId = userId,
               Action = "Request submitted",
               ActionDate = DateTime.UtcNow,
               Comments = $"Books title: {request.Title},Publisher: {request.Publisher},Author: {request.Author}, ISBN : {request.ISBN}"
            };
            await _workflowActionRepository.AddAsync(newWorkflowAction);
            await _workflowActionRepository.SaveAsync();

            var nextStepRole = await _workflowSequenceRepository.GetFirstOrDefaultAsync(wfs => wfs.WorkflowId == workflowId && wfs.StepId == nextStepId.NextStepId);
            var role = await _roleManager.FindByIdAsync(nextStepRole.RequiredRole);
            if (role == null)
            {
                return false; // Role tidak ditemukan
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
            var firstUser = usersInRole.FirstOrDefault();
            var nextRoleEmail = firstUser.Email;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var htmlTemplate = System.IO.File.ReadAllText(@"./Templates/EmailTemplate/AddingBookRequest.html");
            htmlTemplate = htmlTemplate.Replace("{{Name}}", firstUser.UserName);
            htmlTemplate = htmlTemplate.Replace("{{Title}}", request.Title);
            htmlTemplate = htmlTemplate.Replace("{{Author}}", request.Author);
            htmlTemplate = htmlTemplate.Replace("{{Publisher}}", request.Publisher);
            htmlTemplate = htmlTemplate.Replace("{{ISBN}}", request.ISBN);

            var mailData = new MailData
            {
                EmailToName = firstUser.UserName,
                EmailSubject = "Books request to add",
            };

            mailData.EmailToIds.Add(nextRoleEmail);
            mailData.EmailToIds.Add(user.Email);
            mailData.EmailBody = htmlTemplate;

            var emailResult = _emailService.SendEmailAsync(mailData);
            return true;
        }
        public async Task<byte[]> generatereportpdf()

        {

            var bookList = await _bookRepository.GetAllAsync();

            string htmlcontent = String.Empty;

            htmlcontent += "<h1> Book Report </h1>";

            htmlcontent += "<table>";

            htmlcontent += "<tr><td>Id</td><td>Title</td><td>Author</td><td>Category</td><td>Publication Year</td><td>Price</td><td>Publisher</td></tr>";

            bookList.ToList().ForEach(item => {

                htmlcontent += "<tr>";

                htmlcontent += "<td>" + item.Id + "</td>";

                htmlcontent += "<td>" + item.Title + "</td>";

                htmlcontent += "<td>" + item.Author + "</td>";

                htmlcontent += "<td>" + item.Category + "</td>";

                htmlcontent += "<td>" + item.PublicationYear + "</td>";

                htmlcontent += "<td>" + item.Price + "</td>";

                htmlcontent += "<td>" + item.Publisher + "</td>";

                htmlcontent += "</tr>";

            });

            htmlcontent += "</table>";

            var document = new PdfDocument();

            var config = new PdfGenerateConfig();

            config.PageOrientation = PageOrientation.Landscape;

            config.PageSize = PageSize.A4;

            string cssStr = File.ReadAllText(@"./Templates/EmailTemplate/style.css");

            CssData css = PdfGenerator.ParseStyleSheet(cssStr);

            PdfGenerator.AddPdfPages(document, htmlcontent, config, css);

            MemoryStream stream = new MemoryStream();

            document.Save(stream, false);

            byte[] bytes = stream.ToArray();

            return bytes;
        }

        public async Task<int> GetCountingBooks()
        {
            var totalBooks = await _bookRepository.GetCountingBooks();
            return totalBooks;
        }

        public async Task<IEnumerable<BookCategoryDTO>> GetCategoryBooks()
        {
            var categoryBooks = await _bookRepository.GetCategoryBooks();
            return categoryBooks;
        }
    }
}
