using LibrarySystem.Application.IServices;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Domain.IRepositories;
using LibrarySystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using LibrarySystem.Application.DTO.UserDTO;
using System.Globalization;
using LibrarySystem.Application.DTO.Borrowing;
using LibrarySystem.Domain.DTO.Dashboard;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LibrarySystem.Domain.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;

        public BorrowingService(IBorrowingRepository borrowingRepository, IBookRepository bookRepository, IUserRepository userRepository)
        {
            _borrowingRepository = borrowingRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }
        public async Task<Response> CheckOutBooks(UserCheckout usercheckout)
        {
            var foundBook = await _bookRepository.GetFirstOrDefaultAsync(b => b.ISBN == usercheckout.ISBN);
            var foundUser = await _userRepository.GetFirstOrDefaultAsync(u => u.LibraryCardNumber == usercheckout.LibraryCardNumber);
            var today = DateTime.UtcNow;
            if (today > foundUser.LibraryCardExpiringDate)
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Library card is expired"
                };
            }
            var newBorrowing = new Borrowing
            {
                UserId = foundUser.UserId,
                BookId = foundBook.Id,
                BorrowedDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddMonths(1),
            };
            await _borrowingRepository.AddAsync(newBorrowing);
            await _borrowingRepository.SaveAsync();

            return new Response
            {
                Status = "Success",
                Message = "Borrowing request is created"
            };
        }
        public async Task<IEnumerable<object>> GenerateOverdueBorrowingUsersAsync()
        {
            var userDetailBorrows = await _borrowingRepository.GetOverdueBorrowsAsync();
            var overdueUsers = userDetailBorrows.Select(b =>
            {
                int overdueDays = b.ReturnDate.HasValue ?
                (b.ReturnDate.Value.Date - b.DueDate.Date).Days : 0;
                overdueDays = overdueDays > 0 ? overdueDays : 0;
                int penalty = overdueDays > 0 ? overdueDays * 1000 : 0;
                var cultureInfo = new CultureInfo("id-ID");
                var formattedPenalty = string.Format(cultureInfo, "{0:C}", penalty);
                return new
                {
                    User = new
                    {
                        b.User.FirstName,
                        b.User.LastName,
                        b.User.LibraryCardNumber
                    },
                    Details = new
                    {
                        BookTitle = b.Book.Title,
                        ReturnedDate = b.ReturnDate.HasValue ? $"{b.ReturnDate.Value:yyyy-MM-dd}" : "Not Returned",
                        DaysOverdue = overdueDays,
                        Penalty = formattedPenalty
                    }
                };
            })
            .GroupBy(u => new
            {
                u.User.FirstName,
                u.User.LastName,
                u.User.LibraryCardNumber
            })
            .Select(g =>
            {
                return new
                {
                    User = new
                    {
                        FirstName = g.Key.FirstName,
                        LastName = g.Key.LastName,
                        LibraryCardNumber = g.Key.LibraryCardNumber
                    },
                    OverdueDetails = g.Select(x => x.Details).ToList()
                };
            })
            .ToList();
            return overdueUsers;
        }
        public async Task<byte[]> GenerateOverdueBorrowingUsersPdfAsync()
        {
            var overdueBorrows = await _borrowingRepository.GetOverdueBorrowsAsync();

            string htmlContent = "<h1>Overdue Books Report</h1>";
            htmlContent += "<table>";
            htmlContent += "<tr>" +
                "<th>User Id</th>" +
                "<th>First Name</th>" +
                "<th>Last Name</th>" +
                "<th>Library Card Number</th>" +
                "<th>Book Title</th>" +
                "<th>Date Borrowed</th>" +
                "<th>Date Returned</th>" +
                "<th>Days Overdue</th>" +
                "<th>Penalty</th>" +
                "</tr>";

            foreach (var borrow in overdueBorrows)
            {
                var overdueDays = (borrow.ReturnDate - borrow.DueDate)?.TotalDays ?? 0;
                var penalty = overdueDays * 1000;
                var cultureInfo = new CultureInfo("id-ID");
                var numberFormat = cultureInfo.NumberFormat;
                numberFormat.CurrencySymbol = "Rp ";
                var formattedPenalty = string.Format(cultureInfo, "{0:C}", penalty);

                htmlContent += $"<tr>" +
                               $"<td>{borrow.User.UserId}</td>" +
                               $"<td>{borrow.User.FirstName}</td>" +
                               $"<td>{borrow.User.LastName}</td>" +
                               $"<td>{borrow.User.LibraryCardNumber}</td>" +
                               $"<td>{borrow.Book.Title}</td>" +
                               $"<td>{borrow.BorrowedDate:yyyy-MM-dd}</td>" +
                               $"<td>{borrow.ReturnDate:yyyy-MM-dd}</td>" +
                               $"<td>{overdueDays} days</td>" +
                               $"<td>{formattedPenalty}</td>" +
                               $"</tr>";
            }

            htmlContent += "</table>";

            var document = new PdfDocument();
            var config = new PdfGenerateConfig
            {
                PageOrientation = PageOrientation.Landscape,
                PageSize = PageSize.A4
            };

            string cssStr = File.ReadAllText(@"./Templates/EmailTemplate/style.css");
            CssData css = PdfGenerator.ParseStyleSheet(cssStr);
            PdfGenerator.AddPdfPages(document, htmlContent, config, css);

            MemoryStream stream = new MemoryStream();

            document.Save(stream, false);

            byte[] bytes = stream.ToArray();

            return bytes;
        }
        public async Task<IEnumerable<object>> GenerateBorrowedBookAsync(SearchCriteria criteria)
        {
            var borrows = await _borrowingRepository.GetBorrowsByCriteriaAsync(criteria.Startdate, criteria.Enddate);

            var groupedBorrows = borrows.GroupBy(b => b.Book.Category)
                                    .Select(group => new
                                    {
                                        Category = group.Key,
                                        Books = group.ToList(),
                                        Count = group.Count()
                                    });
            return groupedBorrows;
        }

        public async Task<byte[]> GenerateBorrowedBookReportAsync(SearchCriteria criteria)
        {
            var borrows = await _borrowingRepository.GetBorrowsByCriteriaAsync(criteria.Startdate, criteria.Enddate);

            var groupedBorrows = borrows.GroupBy(b => b.Book.Category)
                                    .Select(group => new
                                    {
                                        Category = group.Key,
                                        Books = group.ToList(),
                                        Count = group.Count()
                                    });


            string htmlContent = $"<h1>Sign Out Books Report</h1>";
            htmlContent += $"<p>Time Period: {criteria.Startdate:yyyy-MM-dd} to {criteria.Enddate:yyyy-MM-dd}</p>";
            htmlContent += "<table>";
            htmlContent += "<tr><th>Category</th><th>Number of Books</th></tr>";

            foreach (var group in groupedBorrows)
            {
                htmlContent += $"<tr>" +
                    $"<td>{group.Category}</td>" +
                    $"<td>{group.Count}</td>" +
                    $"</tr>";
            }


            var document = new PdfDocument();
            var config = new PdfGenerateConfig
            {
                PageOrientation = PageOrientation.Portrait,
                PageSize = PageSize.A4
            };

            string cssStr = File.ReadAllText(@"./Templates/EmailTemplate/style.css");
            CssData css = PdfGenerator.ParseStyleSheet(cssStr);
            PdfGenerator.AddPdfPages(document, htmlContent, config, css);

            MemoryStream stream = new MemoryStream();

            document.Save(stream, false);

            byte[] bytes = stream.ToArray();

            return bytes;
        }
        public async Task<IEnumerable<BorrowingOverdueDTO>> GetBorrowsByUserId(int userId)
        {
            var userBorrows = await _borrowingRepository.GetBorrowsByUserIdAsync(userId);
            var borrowsDTO = userBorrows.Select(b =>
            {
                int overdueDays = b.ReturnDate.HasValue ?
                                  (b.ReturnDate.Value - b.DueDate).Days : 0;
                overdueDays = overdueDays > 0 ? overdueDays : 0;
                int penalty = overdueDays > 0 ? overdueDays * 1000 : 0;
                var cultureInfo = new CultureInfo("id-ID");
                var numberFormat = cultureInfo.NumberFormat;
                numberFormat.CurrencySymbol = "Rp ";
                var formattedPenalty = string.Format(cultureInfo, "{0:C}", penalty);

                return new BorrowingOverdueDTO
                {
                    FullName = $"{b.User.FirstName} {b.User.LastName}",
                    BookTitle = b.Book.Title,
                    BorrowedDate = $"{b.BorrowedDate:yyyy - MM - dd}",
                    ReturnedDate = $"{b.ReturnDate:yyyy - MM - dd}",
                    DaysOverdue = overdueDays,
                    Penalty = formattedPenalty
                };
            }).ToList();

            return borrowsDTO;
        }
        public async Task<byte[]> GenerateUserBorrowedReportPdfAsync(int userId)
        {
            var userBorrows = await _borrowingRepository.GetBorrowsByUserIdAsync(userId);

            var user = await _userRepository.GetFirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            string Name = $"{user.FirstName} {user.LastName}";

            string htmlContent = $"<h1>Report for {Name}</h1>";
            htmlContent += "<table>";
            htmlContent += "<tr>" +
                "<th>Full Name</th>" +
                "<th>Book Title</th>" +
                "<th>Date Borrowed</th>" +
                "<th>Date Returned</th>" +
                "<th>Days Overdue</th>" +
                "<th>Penalty</th>" +
                "</tr>";

            foreach (var borrow in userBorrows)
            {

                string fullName = $"{borrow.User.FirstName} {borrow.User.LastName}";
                int overdueDays = (borrow.ReturnDate - borrow.DueDate)?.Days ?? 0;
                int penalty = overdueDays > 0 ? overdueDays * 1000 : 0;
                var cultureInfo = new CultureInfo("id-ID");
                var numberFormat = cultureInfo.NumberFormat;
                numberFormat.CurrencySymbol = "Rp ";
                var formattedPenalty = string.Format(cultureInfo, "{0:C}", penalty);

                htmlContent += $"<tr>" +
                               $"<td>{fullName}</td>" +
                               $"<td>{borrow.Book.Title}</td>" +
                               $"<td>{borrow.BorrowedDate:yyyy-MM-dd}</td>" +
                               $"<td>{borrow.ReturnDate:yyyy-MM-dd}</td>" +
                               $"<td>{(overdueDays > 0 ? overdueDays : 0)} days</td>" +
                               $"<td>{formattedPenalty}</td>" +
                               $"</tr>";
            }

            htmlContent += "</table>";

            var document = new PdfDocument();
            var config = new PdfGenerateConfig
            {
                PageOrientation = PageOrientation.Landscape,
                PageSize = PageSize.A4
            };

            string cssStr = File.ReadAllText(@"./Templates/EmailTemplate/style.css");
            CssData css = PdfGenerator.ParseStyleSheet(cssStr);
            PdfGenerator.AddPdfPages(document, htmlContent, config, css);

            MemoryStream stream = new MemoryStream();

            document.Save(stream, false);

            byte[] bytes = stream.ToArray();

            return bytes;
        }
        public async Task<IEnumerable<ActiveMemberDTO>> GetMostActiveMembers()
        {
            var mostActiveMembers = await _borrowingRepository.GetMostActiveMembers();
            return mostActiveMembers;
        }
        public async Task<IEnumerable<OverdueBooksDTO>> GetOverDueBooks()
        {
            var today = DateTime.UtcNow;
            var overDueBooks = await _borrowingRepository.GetAllAsync(br => br.DueDate < today && br.ReturnDate == null, "Book");
            var overDueBooksDTO = overDueBooks.Select(c => new OverdueBooksDTO
            {
                BookTitle = c.Book.Title,
                OverdueDays = (c.BorrowedDate - c.DueDate).Days
            }).ToList();
            return overDueBooksDTO;
        }
    }
}
