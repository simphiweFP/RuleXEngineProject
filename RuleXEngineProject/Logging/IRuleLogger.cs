using RuleXEngineProject.Engine;
using RuleXEngineProject.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Logging
{
    public interface IRuleLogger
    {
        void LogRuleExecution(string ruleName, RuleResult result, TimeSpan executionTime);
        void LogEngineStart<T>(T input, ExecutionMode mode);
        void LogEngineComplete<T>(RuleEngineResult<T> result);
        void LogError(string ruleName, Exception exception);
    }
}