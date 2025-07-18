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
        public string RuleName => nameof(MedicalConditionRule);
        public RuleResult Evaluate(RuleInputModel input)
        {
            if (input.MedicalConditionStatus != MedicalConditionStatus.None)
            {
                return new RuleResult
                {
                    RuleName = this.RuleName,
                    IsSuccessful = false,
                    Message = "Medical Condition require manual review."
                };
            }

            return new RuleResult 
            {
                RuleName = this.RuleName,
                IsSuccessful = true, 
                Message = "Applicant meets Medical Condition requirements."
            };
        }
    }
}
