using RuleXEngineProject.Models;
using RuleXEngineProject.Rules;
using RuleXEngineProject.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Engine
{
    public class RuleEngine<T> : IRuleEngine<T>
    {
        private readonly IEnumerable<IRule<T>> _rules;
        private readonly ExecutionMode _defaultExecutionMode;
        private readonly IRuleLogger _logger;
        private readonly bool _tracingEnabled;

        public RuleEngine(IEnumerable<IRule<T>> rules, ExecutionMode defaultExecutionMode = ExecutionMode.AllPass, IRuleLogger logger = null, bool tracingEnabled = false)
        {
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
            _defaultExecutionMode = defaultExecutionMode;
            _logger = logger ?? new ConsoleRuleLogger();
            _tracingEnabled = tracingEnabled;
        }

        public RuleEngineResult<T> Evaluate(T input)
        {
            return Evaluate(input, _defaultExecutionMode);
        }

        public RuleEngineResult<T> Evaluate(T input, ExecutionMode mode)
        {
            var stopwatch = Stopwatch.StartNew();
            var results = new List<RuleResult>();
            
            _logger.LogEngineStart(input, mode);

            try
            {
                switch (mode)
                {
                    case ExecutionMode.AllPass:
                        results = ExecuteAllRules(input);
                        break;
                    case ExecutionMode.FirstFail:
                        results = ExecuteUntilFirstFail(input);
                        break;
                    case ExecutionMode.Scored:
                        results = ExecuteAllRules(input);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(mode));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("RuleEngine", ex);
                throw;
            }

            stopwatch.Stop();

            var engineResult = CreateEngineResult(input, results, mode, stopwatch.Elapsed);
            _logger.LogEngineComplete(engineResult);

            return engineResult;
        }

        private List<RuleResult> ExecuteAllRules(T input)
        {
            var results = new List<RuleResult>();

            foreach (var rule in _rules)
            {
                var ruleStopwatch = Stopwatch.StartNew();
                try
                {
                    var result = rule.Evaluate(input);
                    ruleStopwatch.Stop();
                    
                    if (_tracingEnabled)
                    {
                        _logger.LogRuleExecution(GetRuleName(rule), result, ruleStopwatch.Elapsed);
                    }
                    
                    results.Add(result);
                }
                catch (Exception ex)
                {
                    ruleStopwatch.Stop();
                    var ruleName = GetRuleName(rule);
                    _logger.LogError(ruleName, ex);
                    
                    results.Add(new RuleResult
                    {
                        RuleName = ruleName,
                        IsSuccessful = false,
                        Message = $"Rule execution failed: {ex.Message}"
                    });
                }
            }

            return results;
        }

        private List<RuleResult> ExecuteUntilFirstFail(T input)
        {
            var results = new List<RuleResult>();

            foreach (var rule in _rules)
            {
                var ruleStopwatch = Stopwatch.StartNew();
                try
                {
                    var result = rule.Evaluate(input);
                    ruleStopwatch.Stop();
                    
                    if (_tracingEnabled)
                    {
                        _logger.LogRuleExecution(GetRuleName(rule), result, ruleStopwatch.Elapsed);
                    }
                    
                    results.Add(result);

                    if (!result.IsSuccessful)
                    {
                        break; // Stop on first failure
                    }
                }
                catch (Exception ex)
                {
                    ruleStopwatch.Stop();
                    var ruleName = GetRuleName(rule);
                    _logger.LogError(ruleName, ex);
                    
                    results.Add(new RuleResult
                    {
                        RuleName = ruleName,
                        IsSuccessful = false,
                        Message = $"Rule execution failed: {ex.Message}"
                    });
                    break; // Stop on exception
                }
            }

            return results;
        }

        private RuleEngineResult<T> CreateEngineResult(T input, List<RuleResult> results, ExecutionMode mode, TimeSpan executionTime)
        {
            var passedRules = results.Count(r => r.IsSuccessful);
            var failedRules = results.Count(r => !r.IsSuccessful);
            var totalRules = _rules.Count();
            
            var score = totalRules > 0 ? (decimal)passedRules / totalRules * 100 : 0;
            var isSuccessful = DetermineOverallSuccess(results, mode);

            return new RuleEngineResult<T>
            {
                Results = results,
                IsSuccessful = isSuccessful,
                Score = score,
                ExecutionMode = mode,
                Input = input,
                ExecutedAt = DateTime.UtcNow,
                ExecutionTime = executionTime,
                TotalRules = totalRules,
                PassedRules = passedRules,
                FailedRules = failedRules,
                Summary = GenerateSummary(passedRules, failedRules, totalRules, mode)
            };
        }

        private bool DetermineOverallSuccess(List<RuleResult> results, ExecutionMode mode)
        {
            return mode switch
            {
                ExecutionMode.AllPass => results.All(r => r.IsSuccessful),
                ExecutionMode.FirstFail => results.All(r => r.IsSuccessful),
                ExecutionMode.Scored => results.Count(r => r.IsSuccessful) >= results.Count / 2.0, // At least 50% pass
                _ => false
            };
        }

        private string GenerateSummary(int passed, int failed, int total, ExecutionMode mode)
        {
            return mode switch
            {
                ExecutionMode.AllPass => $"Executed all {total} rules: {passed} passed, {failed} failed",
                ExecutionMode.FirstFail => failed > 0 ? $"Stopped at first failure: {passed} passed, {failed} failed" : $"All {passed} rules passed",
                ExecutionMode.Scored => $"Score-based execution: {passed}/{total} rules passed ({(decimal)passed / total * 100:F1}%)",
                _ => $"Unknown execution mode: {passed}/{total} rules passed"
            };
        }

        private string GetRuleName(IRule<T> rule)
        {
            return rule.GetType().Name;
        }
    }
}
