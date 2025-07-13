using RuleXEngineProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Rules
{
    public class OccupationRiskRule : IRule<RuleInputModel>
    {
        public RuleResult Evaluate(RuleInputModel input)
        {
             
            if (input.OccupationRisk == RiskLevel.High)
            {
                return new RuleResult
                {
                    RuleName = "Risk Occupation Rule",
                    IsSuccessful = false,
                    Message = "Acceptable occupation risk: High-risk flagged."
                };
            }
            return new RuleResult { IsSuccessful = true };
        }
    }
}
