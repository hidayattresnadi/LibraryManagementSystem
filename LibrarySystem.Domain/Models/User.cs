using LibrarySystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Privilege { get; set; }
        public string LibraryCardNumber { get; set; }
        public DateTime LibraryCardExpiringDate { get; set; }
        public string? Notes { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Penalty { get; set; }
        [JsonIgnore]
        public virtual ICollection<Borrowing> Borrows { get; set; } = new List<Borrowing>();
    }
}