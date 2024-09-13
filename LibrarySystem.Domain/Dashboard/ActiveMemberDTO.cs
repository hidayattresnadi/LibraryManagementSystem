using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.DTO.Dashboard
{
    public class ActiveMemberDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int BorrowedCount { get; set; }
    }
}
