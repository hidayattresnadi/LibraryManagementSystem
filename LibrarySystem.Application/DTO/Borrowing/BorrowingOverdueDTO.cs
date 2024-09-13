using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTO.Borrowing
{
    public class BorrowingOverdueDTO
    {
        public string FullName { get; set; }
        public string BookTitle { get; set; }
        public string BorrowedDate { get; set; }
        public string? ReturnedDate {  get; set; }
        public int DaysOverdue { get; set; }
        public string Penalty { get; set; }
    }
}
