using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTO.Borrowing
{
    public class SearchCriteria
    {
        public DateOnly Startdate { get; set; }
        public DateOnly Enddate { get; set; }
    }
}
