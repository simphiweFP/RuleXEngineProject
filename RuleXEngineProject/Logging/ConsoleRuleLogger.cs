using RuleXEngineProject.Engine;
using RuleXEngineProject.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Logging
{
    public class ConsoleRuleLogger : IRuleLogger
    {
        public void LogEngineComplete<T>(RuleEngineResult<T> result)
        {
            Console.WriteLine($"[ENGINE] Completed execution in {result.ExecutionTime.TotalMilliseconds}ms");
            Console.WriteLine($"[ENGINE] Mode: {result.ExecutionMode}, Score: {result.Score:F2}, Success: {result.IsSuccessful}");
        }

        public void LogEngineStart<T>(T input, ExecutionMode mode)
        {
            Console.WriteLine($"[ENGINE] Starting rule evaluation with mode: {mode}");
        }

        public void LogError(string ruleName, Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] Rule '{ruleName}' failed with exception: {exception.Message}");
            Console.ResetColor();
        }

        public void LogRuleExecution(string ruleName, RuleResult result, TimeSpan executionTime)
        {
            var status = result.IsSuccessful ? "PASS" : "FAIL";
            var color = result.IsSuccessful ? ConsoleColor.Green : ConsoleColor.Yellow;
            
            Console.ForegroundColor = color;
            Console.WriteLine($"[RULE] {ruleName} - {status} ({executionTime.TotalMilliseconds:F1}ms)");
            Console.ResetColor();
        }
    }
}