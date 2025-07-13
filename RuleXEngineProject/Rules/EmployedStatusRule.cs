using RuleXEngineProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Rules
{
    public class EmployedStatusRule : IRule<RuleInputModel>
    {
        public RuleResult Evaluate(RuleInputModel input)
        {
            if (input.EmploymentStatus != EmploymentStatus.Unemployed)
            {
                return new RuleResult
                {
                    RuleName = "Employed Status Rule",
                    IsSuccessful = false,
                    Message = "Employed Status applicants require premium loading."
                };
            }

            return new RuleResult { IsSuccessful = true };
        }
    }
}
