using RuleXEngineProject.Builder;
using RuleXEngineProject.Engine;
using RuleXEngineProject.Models;
using RuleXEngineProject.Rules;
using System;
using System.Collections.Generic;

namespace RuleXEngineProject.Examples
{
    /// <summary>
    /// Explains what's happening with RuleEngineBuilder<T> : IRuleEngineBuilder<T>
    /// </summary>
    public static class GenericInterfaceExplanation
    {
        /// <summary>
        /// Demonstrates the generic interface implementation concept
        /// </summary>
        public static void ExplainGenericImplementation()
        {
            Console.WriteLine("🔍 Understanding: RuleEngineBuilder<T> : IRuleEngineBuilder<T>");
            Console.WriteLine("=" + new string('=', 65));
            Console.WriteLine();

            Console.WriteLine("📚 What This Means:");
            Console.WriteLine("• RuleEngineBuilder<T> is a GENERIC CLASS");
            Console.WriteLine("• IRuleEngineBuilder<T> is a GENERIC INTERFACE");
            Console.WriteLine("• The class IMPLEMENTS the interface");
            Console.WriteLine("• <T> is a TYPE PARAMETER (placeholder for any type)");
            Console.WriteLine();

            Console.WriteLine("🎯 Why Use Generics?");
            Console.WriteLine("• Type Safety: Compile-time checking");
            Console.WriteLine("• Reusability: Works with any input type");
            Console.WriteLine("• Performance: No boxing/unboxing");
            Console.WriteLine("• IntelliSense: Better IDE support");
            Console.WriteLine();

            DemonstrateTypeSubstitution();
            DemonstrateInterfaceContract();
            DemonstrateTypeSafety();
            DemonstrateReusability();
        }

        /// <summary>
        /// Shows how T gets substituted with actual types
        /// </summary>
        private static void DemonstrateTypeSubstitution()
        {
            Console.WriteLine("🔄 Type Substitution in Action:");
            Console.WriteLine("-" + new string('-', 35));

            // When you write this:
            Console.WriteLine("When you write:");
            Console.WriteLine("  var builder = new RuleEngineBuilder<RuleInputModel>();");
            Console.WriteLine();
            Console.WriteLine("The compiler creates:");
            Console.WriteLine("  RuleEngineBuilder<RuleInputModel> : IRuleEngineBuilder<RuleInputModel>");
            Console.WriteLine();
            Console.WriteLine("Where T = RuleInputModel everywhere in the class!");
            Console.WriteLine();

            // Actual example
            var builder = new RuleEngineBuilder<RuleInputModel>();
            Console.WriteLine($"✅ Created builder for type: {typeof(RuleInputModel).Name}");
            Console.WriteLine();
        }

        /// <summary>
        /// Shows the interface contract
        /// </summary>
        private static void DemonstrateInterfaceContract()
        {
            Console.WriteLine("📋 Interface Contract:");
            Console.WriteLine("-" + new string('-', 20));
            Console.WriteLine();
            Console.WriteLine("IRuleEngineBuilder<T> defines these methods:");
            Console.WriteLine("• AddRule<TRule>() where TRule : IRule<T>");
            Console.WriteLine("• AddRule(IRule<T> rule)");
            Console.WriteLine("• AddRules(IEnumerable<IRule<T>> rules)");
            Console.WriteLine("• WithExecutionMode(ExecutionMode mode)");
            Console.WriteLine("• WithLogger(IRuleLogger logger)");
            Console.WriteLine("• EnableTracing()");
            Console.WriteLine("• Build() -> IRuleEngine<T>");
            Console.WriteLine();
            Console.WriteLine("RuleEngineBuilder<T> MUST implement ALL these methods!");
            Console.WriteLine();
        }

        /// <summary>
        /// Demonstrates type safety benefits
        /// </summary>
        private static void DemonstrateTypeSafety()
        {
            Console.WriteLine("🛡️ Type Safety Benefits:");
            Console.WriteLine("-" + new string('-', 25));
            Console.WriteLine();

            // This works - type safe
            var builder = new RuleEngineBuilder<RuleInputModel>()
                .AddRule<AgeRule>()        // ✅ AgeRule implements IRule<RuleInputModel>
                .AddRule<CountryRule>();   // ✅ CountryRule implements IRule<RuleInputModel>

            Console.WriteLine("✅ This compiles - rules match the input type:");
            Console.WriteLine("   builder.AddRule<AgeRule>()     // AgeRule : IRule<RuleInputModel>");
            Console.WriteLine("   builder.AddRule<CountryRule>() // CountryRule : IRule<RuleInputModel>");
            Console.WriteLine();

            Console.WriteLine("❌ This would NOT compile (if we had such a rule):");
            Console.WriteLine("   builder.AddRule<SomeOtherRule>() // SomeOtherRule : IRule<DifferentModel>");
            Console.WriteLine("   // Compiler error: Type mismatch!");
            Console.WriteLine();
        }

        /// <summary>
        /// Shows reusability with different types
        /// </summary>
        private static void DemonstrateReusability()
        {
            Console.WriteLine("♻️ Reusability with Different Types:");
            Console.WriteLine("-" + new string('-', 35));
            Console.WriteLine();

            Console.WriteLine("The same builder pattern works for ANY type:");
            Console.WriteLine();

            // Example 1: Insurance rules
            Console.WriteLine("1. Insurance Domain:");
            var insuranceBuilder = new RuleEngineBuilder<RuleInputModel>();
            Console.WriteLine($"   RuleEngineBuilder<{typeof(RuleInputModel).Name}>");
            Console.WriteLine("   • Can add insurance-specific rules");
            Console.WriteLine("   • Type-safe for insurance data");
            Console.WriteLine();

            // Example 2: Could work for loan applications
            Console.WriteLine("2. Could work for Loan Domain:");
            Console.WriteLine("   RuleEngineBuilder<LoanApplicationModel>");
            Console.WriteLine("   • Would add loan-specific rules");
            Console.WriteLine("   • Type-safe for loan data");
            Console.WriteLine();

            // Example 3: Could work for any domain
            Console.WriteLine("3. Could work for ANY Domain:");
            Console.WriteLine("   RuleEngineBuilder<YourCustomModel>");
            Console.WriteLine("   • Same fluent API");
            Console.WriteLine("   • Same builder pattern");
            Console.WriteLine("   • Different rules and data");
            Console.WriteLine();
        }

        /// <summary>
        /// Shows what happens behind the scenes
        /// </summary>
        public static void BehindTheScenes()
        {
            Console.WriteLine("🔧 Behind the Scenes:");
            Console.WriteLine("=" + new string('=', 25));
            Console.WriteLine();

            Console.WriteLine("When you write:");
            Console.WriteLine("  var builder = new RuleEngineBuilder<RuleInputModel>();");
            Console.WriteLine();
            Console.WriteLine("The compiler generates something like:");
            Console.WriteLine();
            Console.WriteLine("  public class RuleEngineBuilder_RuleInputModel : IRuleEngineBuilder_RuleInputModel");
            Console.WriteLine("  {");
            Console.WriteLine("      private List<IRule<RuleInputModel>> _rules;");
            Console.WriteLine("      ");
            Console.WriteLine("      public IRuleEngineBuilder<RuleInputModel> AddRule<TRule>()");
            Console.WriteLine("          where TRule : IRule<RuleInputModel>");
            Console.WriteLine("      {");
            Console.WriteLine("          // Implementation");
            Console.WriteLine("      }");
            Console.WriteLine("      ");
            Console.WriteLine("      public IRuleEngine<RuleInputModel> Build()");
            Console.WriteLine("      {");
            Console.WriteLine("          return new RuleEngine<RuleInputModel>(_rules, ...);");
            Console.WriteLine("      }");
            Console.WriteLine("  }");
            Console.WriteLine();
            Console.WriteLine("Every <T> gets replaced with RuleInputModel!");
        }

        /// <summary>
        /// Shows the inheritance hierarchy
        /// </summary>
        public static void InheritanceHierarchy()
        {
            Console.WriteLine("🌳 Inheritance Hierarchy:");
            Console.WriteLine("=" + new string('=', 25));
            Console.WriteLine();
            Console.WriteLine("IRuleEngineBuilder<T>  (Interface - Contract)");
            Console.WriteLine("        ↑");
            Console.WriteLine("        │ implements");
            Console.WriteLine("        │");
            Console.WriteLine("RuleEngineBuilder<T>   (Class - Implementation)");
            Console.WriteLine();
            Console.WriteLine("Benefits:");
            Console.WriteLine("• Interface defines WHAT methods exist");
            Console.WriteLine("• Class defines HOW methods work");
            Console.WriteLine("• Can swap implementations easily");
            Console.WriteLine("• Supports dependency injection");
            Console.WriteLine("• Enables unit testing with mocks");
        }

        /// <summary>
        /// Run all explanations
        /// </summary>
        public static void RunAllExplanations()
        {
            Console.WriteLine("🎓 Understanding Generic Interface Implementation");
            Console.WriteLine("=" + new string('=', 50));
            Console.WriteLine();

            ExplainGenericImplementation();
            BehindTheScenes();
            InheritanceHierarchy();

            Console.WriteLine();
            Console.WriteLine("🎯 Key Takeaways:");
            Console.WriteLine("• <T> is a placeholder for any type");
            Console.WriteLine("• Interface defines the contract");
            Console.WriteLine("• Class provides the implementation");
            Console.WriteLine("• Generics provide type safety and reusability");
            Console.WriteLine("• Compiler does the heavy lifting for you!");
        }
    }

    /// <summary>
    /// Example of what a different domain model might look like
    /// </summary>
    public class LoanApplicationModel
    {
        public decimal RequestedAmount { get; set; }
        public int CreditScore { get; set; }
        public decimal AnnualIncome { get; set; }
        public int YearsEmployed { get; set; }
        public bool HasCollateral { get; set; }
    }

    /// <summary>
    /// Example rule for the loan domain
    /// </summary>
    public class CreditScoreRule : IRule<LoanApplicationModel>
    {
        public string RuleName => nameof(CreditScoreRule);

        public RuleResult Evaluate(LoanApplicationModel input)
        {
            if (input.CreditScore < 600)
            {
                return new RuleResult
                {
                    RuleName = RuleName,
                    IsSuccessful = false,
                    Message = "Credit score too low for approval"
                };
            }

            return new RuleResult
            {
                RuleName = RuleName,
                IsSuccessful = true,
                Message = "Credit score meets requirements"
            };
        }
    }
}