using RuleXEngineProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Rules
{
    class AgeRule : IRule<RuleInputModel>
    {
        public string RuleName => nameof(AgeRule); 

        public RuleResult Evaluate(RuleInputModel input)
        {
            if (input.Age < 21)
            {
                return new RuleResult
                {
                    RuleName = this.RuleName,
                    IsSuccessful = false,
                    Message = "Applicant under 21 requires manual review."
                };
            }

            return new RuleResult
            {
                RuleName = this.RuleName, 
                IsSuccessful = true,
                Message = "Applicant meets age requirements."
            };
        } 
    }
}
