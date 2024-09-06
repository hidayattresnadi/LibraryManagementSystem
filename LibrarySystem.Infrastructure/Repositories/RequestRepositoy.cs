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
    public class RequestRepository : Repository<Request>, IRequestRepository
    {
        private readonly MyDbContext _db;
        public RequestRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
