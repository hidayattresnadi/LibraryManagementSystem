using LibrarySystem.Application.DTO;
using LibrarySystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.IServices
{
    public interface IProcessService
    {
        Task<Response> ReviewRequest(int processId, RequestApproval request);
    }
}
