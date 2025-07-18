using RuleXEngineProject.Engine;
using RuleXEngineProject.Models;
using RuleXEngineProject.Rules;

class Program
{
    static void Main()
    {
        //INPUT COLLECTION 
        Console.WriteLine("=== Insurance Quotation ===");

        Console.Write("Enter Age: ");
        int age = int.Parse(Console.ReadLine());

        Console.Write("Enter Country Code (ZA/US/UK): ");
        CountryCode country = Enum.Parse<CountryCode>(Console.ReadLine().ToUpper());

        Console.Write("Enter Annual Income: ");
        decimal income = decimal.Parse(Console.ReadLine());

        Console.Write("Enter Maximum Cover Required: ");
        decimal maxCover = decimal.Parse(Console.ReadLine());

        Console.Write("Smoking Status (Smoker/NonSmoker): ");
        SmokingStatus smokingStatus = Enum.Parse<SmokingStatus>(Console.ReadLine());

        Console.Write("Occupation Risk Level (Low/Medium/High): ");
        RiskLevel occupationRisk = Enum.Parse<RiskLevel>(Console.ReadLine());

        Console.Write("Medical Condition Status (None/Minor/Severe): ");
        MedicalConditionStatus medicalCondition = Enum.Parse<MedicalConditionStatus>(Console.ReadLine());

        Console.Write("Number of Dependents: ");
        int dependents = int.Parse(Console.ReadLine());

        Console.Write("Employment Status (Employed/SelfEmployed/Unemployed): ");
        EmploymentStatus employmentStatus = Enum.Parse<EmploymentStatus>(Console.ReadLine());


        //BUILD INPUT MODEL 
        var input = new RuleInputModel
        {
            Age = age,
            Country = country,
            Income = income,
            MaxCover = maxCover,
            SmokingStatus = smokingStatus,
            OccupationRisk = occupationRisk,
            MedicalConditionStatus = medicalCondition,
            NumberOfDependents = dependents,
            EmploymentStatus = employmentStatus
        };


        //RULE ENGINE 
        var rules = new List<IRule<RuleInputModel>>
        {
            new AgeRule(),
            new CountryRule(),
            new SmokerRule(),
            new NumberOfDependentsRule(),
            new EmployedStatusRule(),
            new MaximumCoverRule(),
            new MedicalConditionRule(),
            new OccupationRiskRule()
        };

        var engine = new RuleEngine<RuleInputModel>(rules);
        var results = engine.Evaluate(input);


        //RULE EVALUATION RESULTS 
        Console.WriteLine("\n=== Evaluation Results ===");

        foreach (var result in results)
        {
                if (result.IsSuccessful)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[PASSED] {result.RuleName}: {result.Message}");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[FAILED] {result.RuleName}: {result.Message}");
                }

                Console.ResetColor();
        }

        var premiumResult = results.FirstOrDefault(r => r.RuleName == nameof(MaximumCoverRule));

        //QUOTATION SUMMARY 
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║          Quotation Summary             ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.ResetColor();

        bool allRulesPassed = results.TrueForAll(r => r.IsSuccessful);

        if (allRulesPassed)
        {
            Console.WriteLine("Congratulations! Your application is fully approved.");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Estimated Monthly Premium: R{premiumResult.Premium:F2}");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("Some conditions require manual review.");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Estimated Monthly Premium: R{premiumResult.Premium:F2} (Subject to change)");
            Console.ResetColor();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Note: This quote is provisional and subject to underwriter review.");
            Console.ResetColor();
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("──────────────────────────────────────────");
        Console.WriteLine("Thank you for using our Insurance Tool!");
        Console.WriteLine("Have a great day!");
        Console.WriteLine("──────────────────────────────────────────");
        Console.ResetColor();
    }
}