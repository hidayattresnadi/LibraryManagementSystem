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
    public class WorkflowSequenceRepository : Repository<WorkflowSequence>, IWorkflowSequenceRepository
    {
        private readonly MyDbContext _db;
        public WorkflowSequenceRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
