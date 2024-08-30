using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTO.UserDTO
{
    public class UserCheckout
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LibraryCardNumber { get; set; }
        public DateTime LibraryCardExpiringDate { get; set; }
        public bool HasUnpaidPenalty { get; set; }
        public int BooksNotReturned { get; set; }
    }
}
