using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.DTO.Dashboard
{
    public class OverdueBooksDTO
    {
        public string BookTitle { get; set; }
        public int OverdueDays { get; set; }
    }
}
