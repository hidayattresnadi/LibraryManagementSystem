using LibrarySystem.Application.DTO.NextStepRules;
using LibrarySystem.Application.IServices;
using LibrarySystem.Application.Repositories;
using LibrarySystem.Domain.IRepositories;
using LibrarySystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.Services
{
    public class NextStepRulesService : INextStepRulesService
    {
        private readonly INextStepRulesRepository _nextStepRulesRepository;
        public NextStepRulesService(INextStepRulesRepository nextStepRulesRepository )
        {
            _nextStepRulesRepository = nextStepRulesRepository;
        }
        public async Task<bool> AddNextStepRules(NextStepRulesAdd request)
        {
            var newNextStepRules = new NextStepRules
            {
                ConditionType = request.ConditionType,
                ConditionValue = request.ConditionValue,
                CurrentStepId = request.CurrentStepId,
                NextStepId = request.NextStepId
            };
            await _nextStepRulesRepository.AddAsync(newNextStepRules);
            await _nextStepRulesRepository.SaveAsync();
            return true;
        }
    }
}
