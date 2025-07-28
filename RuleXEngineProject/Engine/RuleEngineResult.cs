using RuleXEngineProject.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Engine
{
    public class RuleEngineResult<T>
    {
        public List<RuleResult> Results { get; set; } = new List<RuleResult>();
        public bool IsSuccessful { get; set; }
        public decimal Score { get; set; }
        public ExecutionMode ExecutionMode { get; set; }
        public T Input { get; set; }
        public DateTime ExecutedAt { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public int TotalRules { get; set; }
        public int PassedRules { get; set; }
        public int FailedRules { get; set; }
        public string Summary { get; set; }
    }
}