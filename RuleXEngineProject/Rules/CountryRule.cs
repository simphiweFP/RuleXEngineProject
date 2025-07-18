using RuleXEngineProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Rules
{
    class CountryRule : IRule<RuleInputModel>
    {
        public string RuleName => nameof(CountryRule); 

        public RuleResult Evaluate(RuleInputModel input)
        {
            if (input.Country!= CountryCode.ZA)
            {
                return new RuleResult
                {
                    RuleName = this.RuleName,
                    IsSuccessful = false,
                    Message = "Country Code applicants require manual review."
                };
            }

            return new RuleResult
            {
                RuleName = this.RuleName, 
                IsSuccessful = true,
                Message = "Applicant meets Country Code requirements."
            };
        }  
    }
}
