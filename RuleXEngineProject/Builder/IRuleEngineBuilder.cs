using RuleXEngineProject.Engine;
using RuleXEngineProject.Logging;
using RuleXEngineProject.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Builder
{
    public interface IRuleEngineBuilder<T>
    {
        IRuleEngineBuilder<T> AddRule<TRule>() where TRule : class, IRule<T>, new();
        IRuleEngineBuilder<T> AddRule(IRule<T> rule);
        IRuleEngineBuilder<T> AddRules(IEnumerable<IRule<T>> rules);
        IRuleEngineBuilder<T> WithExecutionMode(ExecutionMode mode);
        IRuleEngineBuilder<T> WithLogger(IRuleLogger logger);
        IRuleEngineBuilder<T> EnableTracing();
        IRuleEngine<T> Build();
    }
}