﻿using LibrarySystem.Domain.IRepositories;
using LibrarySystem.Domain.Models;
using LibrarySystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure.Repositories
{
    public class ProcessRepository : Repository<Process>, IProcessRepository
    {
        private readonly MyDbContext _db;
        public ProcessRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Process>> GetProcessBasedOnRole(List<string> roles)
        {
            var process = await _db.Processes.Include(p =>p.WorkflowSequence).ThenInclude(w =>w.Role).
                Include(p=>p.Workflow).Include(p=>p.Requester).Where(p=> roles.Contains(p.WorkflowSequence.Role.Name))
                .ToListAsync();
            return process;
        }
    }
}
