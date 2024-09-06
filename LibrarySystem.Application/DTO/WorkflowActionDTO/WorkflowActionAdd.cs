using LibrarySystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTO.WorkflowActionDTO
{
    public class WorkflowActionAdd
    {
        public int ProcessId { get; set; }
        public int StepId { get; set; }
        public string ActorId { get; set; }
        public string Action { get; set; }
        public DateTime ActionDate { get; set; }
        public string Comments { get; set; }
    }
}
