using LibrarySystem.Domain.DTO.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.IServices
{
    public interface IDashboardService
    {
        Task<DashboardDTO> GetDashboardInfo();
    }
}
