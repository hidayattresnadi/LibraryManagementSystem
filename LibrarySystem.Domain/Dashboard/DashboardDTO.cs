using LibrarySystem.Domain.DTO.ProcessDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.DTO.Dashboard
{
    public class DashboardDTO
    {
        public int TotalBooks { get; set; }
        public IEnumerable<ActiveMemberDTO> MostActiveMembers { get; set; }
        public IEnumerable<OverdueBooksDTO> OverdueBooks { get; set; }
        public IEnumerable<BookCategoryDTO> BooksPerCategory { get; set; }
        public IEnumerable<ProcessDetailDTO> ProcessesCurentUser { get; set; }
    }
}
