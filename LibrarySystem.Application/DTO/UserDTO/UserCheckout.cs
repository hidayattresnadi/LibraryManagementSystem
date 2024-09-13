using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTO.UserDTO
{
    public class UserCheckout
    {
        public string ISBN {  get; set; } 
        public string LibraryCardNumber { get; set; }
    }
}
