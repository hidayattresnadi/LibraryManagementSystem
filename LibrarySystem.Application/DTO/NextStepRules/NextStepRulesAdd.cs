using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.DTO.NextStepRules
{
    public class NextStepRulesAdd
    {
        public int CurrentStepId { get; set; }
        public int NextStepId { get; set; }
        public string ConditionType { get; set; }
        public string ConditionValue { get; set; }
    }
}
