using LibrarySystem.Domain.IRepositories;
using LibrarySystem.Domain.Models;
using LibrarySystem.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure.Repositories
{
    public class BorrowingRepository : Repository<Borrowing>, IBorrowingRepository
    {
        private readonly MyDbContext _db;
        public BorrowingRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
