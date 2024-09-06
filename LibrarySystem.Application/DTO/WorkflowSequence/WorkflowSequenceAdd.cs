using LibrarySystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTO.WorkflowSequence
{
    public class WorkflowSequenceAdd
    {
        public int WorkflowId { get; set; }
        public int StepOrder { get; set; }
        public string StepName { get; set; }
        public string? RequiredRole { get; set; }
    }
}
