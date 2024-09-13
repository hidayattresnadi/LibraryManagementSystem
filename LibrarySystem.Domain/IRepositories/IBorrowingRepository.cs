using LibrarySystem.Application.Repositories;
using LibrarySystem.Domain.DTO.Dashboard;
using LibrarySystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.IRepositories
{
    public interface IBorrowingRepository : IRepository<Borrowing>
    {
        Task<IEnumerable<Borrowing>> GetOverdueBorrowsAsync();
        Task<IEnumerable<Borrowing>> GetBorrowsByCriteriaAsync(DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<Borrowing>> GetBorrowsByUserIdAsync(int userId);
        Task<IEnumerable<ActiveMemberDTO>> GetMostActiveMembers();
    }
}
