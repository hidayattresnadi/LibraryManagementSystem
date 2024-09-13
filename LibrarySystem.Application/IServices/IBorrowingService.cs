using LibrarySystem.Application.DTO.Borrowing;
using LibrarySystem.Domain.DTO.Dashboard;
using LibrarySystem.Application.DTO.UserDTO;
using LibrarySystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.IServices
{
    public interface IBorrowingService
    {
        Task<Response> CheckOutBooks(UserCheckout userCheckout);
        Task<byte[]> GenerateOverdueBorrowingUsersPdfAsync();
        Task<byte[]> GenerateBorrowedBookReportAsync(SearchCriteria criteria);
        Task<byte[]> GenerateUserBorrowedReportPdfAsync(int userId);
        Task<IEnumerable<ActiveMemberDTO>> GetMostActiveMembers();
        Task<IEnumerable<OverdueBooksDTO>> GetOverDueBooks();
        Task<IEnumerable<object>> GenerateBorrowedBookAsync(SearchCriteria criteria);
        Task<IEnumerable<BorrowingOverdueDTO>> GetBorrowsByUserId(int userId);
        Task<IEnumerable<object>> GenerateOverdueBorrowingUsersAsync();
    }
}
