using LibrarySystem.Application.DTO.UserDTO;
using LibrarySystem.Application.DTO.WorkflowDTO;
using LibrarySystem.Application.IServices;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Domain.IRepositories;
using LibrarySystem.Domain.Models;
using LibrarySystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IWorkflowRepository _workflowRepository;
        public WorkflowService(IWorkflowRepository workflowRepository)
        {
            _workflowRepository = workflowRepository;
        }
        public async Task<bool> AddWorkflow(WorkflowRequest inputWorkflow)
        {
            var newWorkflow = new Workflow
            {
                WorkflowName = inputWorkflow.WorkflowName,
                Description = inputWorkflow.Description
            };
            await _workflowRepository.AddAsync(newWorkflow);
            await _workflowRepository.SaveAsync();
            return true;
        }
    }
}
