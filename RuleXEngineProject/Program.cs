using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RuleXEngineProject.Builder;
using RuleXEngineProject.Engine;
using RuleXEngineProject.Extensions;
using RuleXEngineProject.Logging;
using RuleXEngineProject.Models;
using RuleXEngineProject.Rules;
using RuleXEngineProject.Examples;

class Program
{
    static void Main()
    {
        // Show quick start examples first
        Console.WriteLine("🧠 SmartRules - Strategy-Based Rule Engine");
        Console.WriteLine("=" + new string('=', 50));
        Console.WriteLine();
        
        Console.WriteLine("Would you like to see examples first? (y/n)");
        var showExamples = Console.ReadLine()?.ToLower();
        
        if (showExamples == "y" || showExamples == "yes")
        {
            QuickStartGuide.QuickStart();
            QuickStartGuide.ExecutionModeShowcase();
            
            Console.WriteLine("\nPress any key to continue to the interactive demo...");
            Console.ReadKey();
            Console.Clear();
        }

        // Setup Dependency Injection
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddRuleEngine<RuleInputModel>();
                services.AddRule<RuleInputModel, AgeRule>();
                services.AddRule<RuleInputModel, CountryRule>();
                services.AddRule<RuleInputModel, SmokerRule>();
                services.AddRule<RuleInputModel, NumberOfDependentsRule>();
                services.AddRule<RuleInputModel, EmployedStatusRule>();
                services.AddRule<RuleInputModel, MaximumCoverRule>();
                services.AddRule<RuleInputModel, MedicalConditionRule>();
                services.AddRule<RuleInputModel, OccupationRiskRule>();
            })
            .Build();

        //INPUT COLLECTION 
        Console.WriteLine("🧠 SmartRules - Interactive Insurance Quotation");
        Console.WriteLine("=" + new string('=', 50));
        Console.WriteLine();

        Console.Write("Enter Age: ");
        int age = int.Parse(Console.ReadLine() ?? "25");

        Console.Write("Enter Country Code (ZA/US/UK): ");
        CountryCode country = Enum.Parse<CountryCode>(Console.ReadLine()?.ToUpper() ?? "ZA");

        Console.Write("Enter Annual Income: ");
        decimal income = decimal.Parse(Console.ReadLine() ?? "50000");

        Console.Write("Enter Maximum Cover Required: ");
        decimal maxCover = decimal.Parse(Console.ReadLine() ?? "100000");

        Console.Write("Smoking Status (Smoker/NonSmoker): ");
        SmokingStatus smokingStatus = Enum.Parse<SmokingStatus>(Console.ReadLine() ?? "NonSmoker");

        Console.Write("Occupation Risk Level (Low/Medium/High): ");
        RiskLevel occupationRisk = Enum.Parse<RiskLevel>(Console.ReadLine() ?? "Low");

        Console.Write("Medical Condition Status (None/Minor/Severe): ");
        MedicalConditionStatus medicalCondition = Enum.Parse<MedicalConditionStatus>(Console.ReadLine() ?? "None");

        Console.Write("Number of Dependents: ");
        int dependents = int.Parse(Console.ReadLine() ?? "2");

        Console.Write("Employment Status (Employed/SelfEmployed/Unemployed): ");
        EmploymentStatus employmentStatus = Enum.Parse<EmploymentStatus>(Console.ReadLine() ?? "Employed");

        Console.Write("Execution Mode (AllPass/FirstFail/Scored): ");
        ExecutionMode executionMode = Enum.Parse<ExecutionMode>(Console.ReadLine() ?? "AllPass");

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

        // FLUENT RULE ENGINE SETUP
        var builder = host.Services.GetRequiredService<IRuleEngineBuilder<RuleInputModel>>();
        var engine = builder
            .AddRule<AgeRule>()
            .AddRule<CountryRule>()
            .AddRule<SmokerRule>()
            .AddRule<NumberOfDependentsRule>()
            .AddRule<EmployedStatusRule>()
            .AddRule<MaximumCoverRule>()
            .AddRule<MedicalConditionRule>()
            .AddRule<OccupationRiskRule>()
            .WithExecutionMode(executionMode)
            .WithLogger(new ConsoleRuleLogger())
            .EnableTracing()
            .Build();

        var engineResult = engine.Evaluate(input);
        var results = engineResult.Results;

        //RULE EVALUATION RESULTS 
        Console.WriteLine($"\n🔍 Evaluation Results ({engineResult.ExecutionMode} Mode)");
        Console.WriteLine("=" + new string('=', 50));
        Console.WriteLine($"Execution Time: {engineResult.ExecutionTime.TotalMilliseconds:F1}ms");
        Console.WriteLine($"Score: {engineResult.Score:F1}%");
        Console.WriteLine($"Summary: {engineResult.Summary}");
        Console.WriteLine();

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
        Console.WriteLine("💼 Quotation Summary");
        Console.WriteLine("=" + new string('=', 30));
        Console.ResetColor();

        bool allRulesPassed = engineResult.IsSuccessful;

        if (allRulesPassed)
        {
            Console.WriteLine("🎉 Congratulations! Your application is fully approved.");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"💰 Estimated Monthly Premium: R{premiumResult?.Premium ?? 500:F2}");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("⚠️  Some conditions require manual review.");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"💰 Estimated Monthly Premium: R{premiumResult?.Premium ?? 500:F2} (Subject to change)");
            Console.ResetColor();

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📝 Note: This quote is provisional and subject to underwriter review.");
            Console.ResetColor();
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("=" + new string('=', 50));
        Console.WriteLine("🙏 Thank you for using SmartRules Insurance Tool!");
        Console.WriteLine("✨ Have a great day!");
        Console.WriteLine("=" + new string('=', 50));
        Console.ResetColor();

        host.Dispose();
    }
}