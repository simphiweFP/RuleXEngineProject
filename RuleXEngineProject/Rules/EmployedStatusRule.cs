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
        public string RuleName => nameof(EmployedStatusRule); 
        public RuleResult Evaluate(RuleInputModel input)
        {
            if (input.EmploymentStatus == EmploymentStatus.Unemployed)
            {
                return new RuleResult
                {
                    RuleName = this.RuleName,
                    IsSuccessful = false,
                    Message = "Employed Status applicants require manual review."
                };
            }

            return new RuleResult
            {
                RuleName = this.RuleName,
                IsSuccessful = true,
                Message = "Applicant Employment Status meets requirements."
            };
        }
    }
}
