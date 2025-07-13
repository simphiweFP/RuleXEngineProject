using RuleXEngineProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Rules
{
    public class MedicalConditionRule : IRule<RuleInputModel>
    {
        public RuleResult Evaluate(RuleInputModel input)
        {
            if (input.MedicalConditionStatus != MedicalConditionStatus.None)
            {
                return new RuleResult
                {
                    RuleName = "Medical Condition Rule",
                    IsSuccessful = false,
                    Message = "Medical Condition require premium loading."
                };
            }

            return new RuleResult { IsSuccessful = true };
        }
    }
}
