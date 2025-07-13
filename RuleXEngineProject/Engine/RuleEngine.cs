using RuleXEngineProject.Models;
using RuleXEngineProject.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Engine
{
    public class RuleEngine<T>
    {
        private readonly IEnumerable<IRule<T>> _rules;

        public RuleEngine(IEnumerable<IRule<T>> rules)
        {
            _rules = rules;
        }
        public List<RuleResult> Evaluate(T input)
        {
            return _rules.Select(rule => rule.Evaluate(input)).ToList();
        }
    }
}
