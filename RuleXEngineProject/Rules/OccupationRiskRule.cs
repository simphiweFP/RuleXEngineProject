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
        public string RuleName => nameof(OccupationRiskRule);
        public RuleResult Evaluate(RuleInputModel input)
        {
             
            if (input.OccupationRisk == RiskLevel.High)
            {
                return new RuleResult
                {
                    RuleName = this.RuleName,
                    IsSuccessful = false,
                    Message = "Acceptable occupation risk: High-risk flagged."
                };
            }
            return new RuleResult 
            {
                RuleName = this.RuleName, 
                IsSuccessful = true,
                Message = "Applicant meets Occupation Risk requirements."
            };
        }
    }
}
