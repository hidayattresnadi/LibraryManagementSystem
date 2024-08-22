using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Models
{
    public class Borrowing
    {
        public int BorrowingId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool HasUnpaidPenalty { get; set; } = false;
        public decimal? PenaltyAmount { get; set; }
        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }
}
