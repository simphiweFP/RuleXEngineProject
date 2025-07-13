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
        public RuleResult Evaluate(RuleInputModel input)
        {
            if (input.Age < 21)
            {
                return new RuleResult
                {
                    RuleName = "AgeRule",
                    IsSuccessful = false,
                    Message = "Applicant under 21 requires manual review."
                };
            }

            return new RuleResult { IsSuccessful = true };
        } 
    }
}
