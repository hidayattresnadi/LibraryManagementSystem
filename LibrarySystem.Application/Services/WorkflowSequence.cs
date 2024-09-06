using LibrarySystem.Application.DTO.WorkflowSequence;
using LibrarySystem.Application.IServices;
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
    public class WorkflowSequenceService :IWorkflowSequenceService
    {
        private readonly IWorkflowSequenceRepository _workflowSequenceRepository;
        public WorkflowSequenceService(IWorkflowSequenceRepository workflowSequenceRepository)
        {
            _workflowSequenceRepository = workflowSequenceRepository;
        }
        public async Task<bool> AddWorkflowSequence(WorkflowSequenceAdd workflowSequenceAdd)
        {
            var workflowSequence = new WorkflowSequence
            {
                RequiredRole = workflowSequenceAdd.RequiredRole,
                StepName = workflowSequenceAdd.StepName,
                StepOrder = workflowSequenceAdd.StepOrder,
                WorkflowId = workflowSequenceAdd.WorkflowId
            };
            await _workflowSequenceRepository.AddAsync(workflowSequence);
            await _workflowSequenceRepository.SaveAsync();
            return true;
        }
    }
}
