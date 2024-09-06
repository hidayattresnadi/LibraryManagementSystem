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
    public class WorkflowActionRepository : Repository<WorkflowAction>, IWorkflowActionRepository
    {
        private readonly MyDbContext _db;
        public WorkflowActionRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
