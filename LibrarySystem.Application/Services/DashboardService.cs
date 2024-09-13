using LibrarySystem.Domain.DTO.Dashboard;
using LibrarySystem.Application.IServices;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Domain.IRepositories;
using LibrarySystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using LibrarySystem.Domain.DTO.ProcessDTO;

namespace LibrarySystem.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IBookService _bookService;
        private readonly IBorrowingService _borrowingService;
        private readonly IProcessRepository _processRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardService(IBookService bookService, IBorrowingService borrowingService, IProcessRepository processRepository,
                                IHttpContextAccessor httpContextAccessor)
        {
            _bookService = bookService;
            _borrowingService = borrowingService;
            _processRepository = processRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DashboardDTO> GetDashboardInfo()
        {
            var userRoles = _httpContextAccessor.HttpContext?.User?.Claims
                            .Where(c => c.Type == ClaimTypes.Role)
                            .Select(c => c.Value)
                            .ToList();
            IEnumerable<Process> processList = Enumerable.Empty<Process>();
            processList = await _processRepository.GetProcessBasedOnRole(userRoles);
            var processUsersDTO = processList.Select(p => new ProcessDetailDTO
            {
                ProcessId = p.ProcessId,
                WorkflowName = p.Workflow.WorkflowName,
                Requester = p.Requester.UserName,
                RequestDate = p.RequestDate,
                Status = p.Status,
                CurrentStep = p.WorkflowSequence.StepName
            }).ToList();
            var countingBooks = await _bookService.GetCountingBooks();
            var categoryBooks = await _bookService.GetCategoryBooks();
            var mostActiveMembers = await _borrowingService.GetMostActiveMembers();
            var overDueBooks = await _borrowingService.GetOverDueBooks();

            var dashboard = new DashboardDTO
            {
                TotalBooks = countingBooks,
                MostActiveMembers = mostActiveMembers,
                OverdueBooks = overDueBooks,
                BooksPerCategory = categoryBooks,
                ProcessesCurentUser = processUsersDTO
            };
            return dashboard;
        }
            
    }
}
