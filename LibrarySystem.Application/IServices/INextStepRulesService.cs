using LibrarySystem.Application.DTO.NextStepRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.IServices
{
    public interface INextStepRulesService
    {
        Task<bool> AddNextStepRules(NextStepRulesAdd request);
    }
}
