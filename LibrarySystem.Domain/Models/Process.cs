using LibrarySystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Models
{
    public class Process
    {
        [Key]
        public int ProcessId { get; set; }
        [ForeignKey("Workflow")]
        public int WorkflowId { get; set; }
        public virtual Workflow Workflow { get; set; }
        [ForeignKey("AspNetUsers")]
        public string RequesterId { get; set; }
        public virtual AppUser Requester { get; set; }
        public string RequestType { get; set; }
        public string Status { get; set; }
        [ForeignKey("WorkflowSequence")]
        public int CurrentStepId { get; set; }
        public virtual WorkflowSequence WorkflowSequence { get; set; }
        [ForeignKey("Request")]
        public int RequestId { get; set; }
        public virtual Request Request { get; set; }
        public DateTime RequestDate { get; set; }
        public virtual ICollection<WorkflowAction> WorkflowActions { get; set; }
        public virtual ICollection<BookRequest> BookRequests { get; set; }
    }
}