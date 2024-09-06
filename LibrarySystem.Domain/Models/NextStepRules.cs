﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Models
{
    public class NextStepRules
    {
        public int RuleId { get; set; }
        [ForeignKey("WorflowSequence")]
        public int CurrentStepId { get; set; }
        public virtual WorkflowSequence CurrentStep { get; set; }
        [ForeignKey("WorflowSequence")]
        public int NextStepId { get; set; }
        public virtual WorkflowSequence NextStep { get; set; }
        public string ConditionType { get; set; }
        public string ConditionValue { get; set; }
    }
}
