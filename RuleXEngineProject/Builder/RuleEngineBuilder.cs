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
    public class RuleEngineBuilder<T> : IRuleEngineBuilder<T>
    {
        private readonly List<IRule<T>> _rules = new List<IRule<T>>();
        private ExecutionMode _executionMode = ExecutionMode.AllPass;
        private IRuleLogger _logger = new ConsoleRuleLogger();
        private bool _tracingEnabled = false;

        public IRuleEngineBuilder<T> AddRule<TRule>() where TRule : class, IRule<T>, new()
        {
            _rules.Add(new TRule());
            return this;
        }

        public IRuleEngineBuilder<T> AddRule(IRule<T> rule)
        {
            if (rule == null)
                throw new ArgumentNullException(nameof(rule));
            
            _rules.Add(rule);
            return this;
        }

        public IRuleEngineBuilder<T> AddRules(IEnumerable<IRule<T>> rules)
        {
            if (rules == null)
                throw new ArgumentNullException(nameof(rules));
            
            _rules.AddRange(rules);
            return this;
        }

        public IRuleEngineBuilder<T> WithExecutionMode(ExecutionMode mode)
        {
            _executionMode = mode;
            return this;
        }

        public IRuleEngineBuilder<T> WithLogger(IRuleLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            return this;
        }

        public IRuleEngineBuilder<T> EnableTracing()
        {
            _tracingEnabled = true;
            return this;
        }

        public IRuleEngine<T> Build()
        {
            if (!_rules.Any())
                throw new InvalidOperationException("At least one rule must be added to the engine.");

            return new RuleEngine<T>(_rules, _executionMode, _logger, _tracingEnabled);
        }
    }
}