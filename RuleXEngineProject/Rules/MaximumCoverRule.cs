using RuleXEngineProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Rules
{
    public class MaximumCoverRule : IRule<RuleInputModel>
    {
        public string RuleName => nameof(MaximumCoverRule); 

        public RuleResult Evaluate(RuleInputModel input)
        {
            return new RuleResult
            {
                RuleName = this.RuleName,
                IsSuccessful = true,
                Message = "Premium calculated based on risk factors.",
                Premium = CalculatePremiumImpact(input)
            };
        }

        private decimal CalculatePremiumImpact(RuleInputModel input)
        {
            decimal basePremium = 500m;

            if (input.SmokingStatus == SmokingStatus.Smoker)
            {
                basePremium *= 1.5m;
            }

            if (input.MedicalConditionStatus == MedicalConditionStatus.Minor)
            {
                basePremium *= 1.4m;
            }

            if (input.OccupationRisk == RiskLevel.Medium)
            {
                basePremium *= 1.3m;
            }

            if (input.Age > 60)
            {
                basePremium *= 1.2m;
            }

            return basePremium;
        }
    }
}
