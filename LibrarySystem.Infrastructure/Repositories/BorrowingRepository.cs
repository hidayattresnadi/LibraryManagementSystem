using LibrarySystem.Domain.DTO.Dashboard;
using LibrarySystem.Domain.IRepositories;
using LibrarySystem.Domain.Models;
using LibrarySystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Repositories
{
    public class BorrowingRepository : Repository<Borrowing>, IBorrowingRepository
    {
        private readonly MyDbContext _db;
        public BorrowingRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Borrowing>> GetOverdueBorrowsAsync()
        {
            var borrowingItems = await _db.Borrowings.Where(b => b.ReturnDate > b.DueDate && b.PenaltyAmount > 0)
                                       .Include("User")
                                       .Include("Book")
                                       .OrderBy(br => br.UserId)
                                       .ToListAsync();
            return borrowingItems;
        }
        public async Task<IEnumerable<Borrowing>> GetBorrowsByCriteriaAsync(DateOnly startDate, DateOnly endDate)
        {
            DateTime startDateTime = DateTime.SpecifyKind(startDate.ToDateTime(new TimeOnly(0, 0)), DateTimeKind.Utc);
            DateTime endDateTime = DateTime.SpecifyKind(endDate.ToDateTime(new TimeOnly(23, 59, 59)), DateTimeKind.Utc);

            var borrowsQuery = _db.Borrowings
                .Include("Book")
                .Where(br => br.BorrowedDate >= startDateTime && br.BorrowedDate <= endDateTime);
            return await borrowsQuery.ToListAsync();
        }

        public async Task<IEnumerable<Borrowing>> GetBorrowsByUserIdAsync(int userId)
        {
            return await _db.Borrowings
                             .Where(b => b.UserId == userId)
                             .Include("User")
                             .Include("Book")
                             .OrderBy(b => b.UserId)
                             .ToListAsync();
        }
        public async Task<IEnumerable<ActiveMemberDTO>> GetMostActiveMembers()
        {
            var mostActiveMembers = await _db.Borrowings.Include("User").GroupBy(b => new { b.UserId, b.User.FirstName, b.User.LastName }).OrderByDescending(g => g.Count()).Take(10).Select(g => new ActiveMemberDTO { UserId = g.Key.UserId, BorrowedCount = g.Count(), Name = $"{g.Key.FirstName} {g.Key.LastName}" }).ToListAsync();
            return mostActiveMembers;
        }
    }
}
