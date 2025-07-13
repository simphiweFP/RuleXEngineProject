using RuleXEngineProject.Engine;
using RuleXEngineProject.Models;
using RuleXEngineProject.Rules;

var input = new RuleInputModel
{
    Age = 35,
    Country = CountryCode.ZA,
    Income = 60000,
    MaxCover = 500000,
    SmokingStatus = SmokingStatus.NonSmoker,
    OccupationRisk = RiskLevel.Medium,
    MedicalConditionStatus = MedicalConditionStatus.Minor,
    NumberOfDependents = 2,
    EmploymentStatus = EmploymentStatus.Employed
};

var rules = new List<IRule<RuleInputModel>>
{
    new AgeRule(),
    new CountryRule(),
    new SmokerRule(),
    new NumberOfDependentsRule(),
    new EmployedStatusRule(),
    new MaximumCoverRule(),
    new MedicalConditionRule(),
    new OccupationRiskRule(),

};

var engine = new RuleEngine<RuleInputModel>(rules);
var results = engine.Evaluate(input);

foreach (var result in results)
{
    Console.WriteLine($"{result.RuleName}: {(result.IsSuccessful ? "✅" : "❌")} - {result.Message}");
}

