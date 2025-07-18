using RuleXEngineProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Rules
{
    public class SmokerRule : IRule<RuleInputModel>
    {
        public string RuleName => nameof(SmokerRule);
        public RuleResult Evaluate(RuleInputModel input)
        {
            if (input.SmokingStatus != SmokingStatus.NonSmoker)
            {
                return new RuleResult
                {
                    RuleName = "Smoker Rule",
                    IsSuccessful = false,
                    Message = "Smoker applicants require manual review."
                };
            }
            return new RuleResult
            {
                RuleName = this.RuleName, 
                IsSuccessful = true,
                Message = "Applicant meets Smoker requirements."
            };
        }
    }
}
