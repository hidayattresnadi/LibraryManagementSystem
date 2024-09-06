using LibrarySystem.Application.DTO.WorkflowSequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.IServices
{
    public interface IWorkflowSequenceService
    {
        Task<bool> AddWorkflowSequence(WorkflowSequenceAdd workflowSequenceAdd);
    }
}
