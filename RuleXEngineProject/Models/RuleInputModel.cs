using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Models
{
    public class RuleInputModel
    {
        public int Age { get; set; }
        public decimal MaxCover { get; set; }
        public RiskLevel OccupationRisk { get; set; }
        public CountryCode Country { get; set; }
        public decimal Income { get; set; }
        public SmokingStatus SmokingStatus { get; set; }
        public MedicalConditionStatus MedicalConditionStatus { get; set; }
        public int NumberOfDependents { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
    }
}
