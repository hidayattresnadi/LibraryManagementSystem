using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Domain.Models
{
    public class AppUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpire { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<WorkflowAction> WorkflowActions { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
    }
}
