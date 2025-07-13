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
        public RuleResult Evaluate(RuleInputModel input)
        {
            if (input.Country!= CountryCode.ZA)
            {
                return new RuleResult
                {
                    RuleName = "Country Rule",
                    IsSuccessful = false,
                    Message = "Smoker applicants require manual review."
                };
            }

            return new RuleResult { IsSuccessful = true };
        }  
    }
}
