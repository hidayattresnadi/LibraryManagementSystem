using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTO.UserDTO
{
    public class UserLateReturn
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public  string LibraryCardNumber { get; set; }
        public string BookTitle { get; set; }
        public string BorrowedDate { get; set; }
        public string ReturnedDate { get; set; }
        public int DaysOverdue { get; set; }
        public string Penalty {  get; set; }
    }
}
