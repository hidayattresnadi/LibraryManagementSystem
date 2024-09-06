using LibrarySystem.Application.DTO.WorkflowDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.IServices
{
    public interface IWorkflowService
    {
        Task<bool> AddWorkflow(WorkflowRequest inputWorkflow);
    }
}
