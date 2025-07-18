using RuleXEngineProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Rules
{
   public class NumberOfDependentsRule : IRule<RuleInputModel>
    {
        public string RuleName => nameof(NumberOfDependentsRule);
        public RuleResult Evaluate(RuleInputModel input)
        {
            if (input.NumberOfDependents > 5)
            {
                return new RuleResult
                {
                    RuleName = "Number Of Dependents Rule",
                    IsSuccessful = false,
                    Message = "Number Of Dependent exceed requires manual review."
                };
            }

            return new RuleResult 
            {
                RuleName = this.RuleName, 
                IsSuccessful = true,
                Message = "Applicant Number Of Dependent meets requirements."
            };
        }
    }
}
