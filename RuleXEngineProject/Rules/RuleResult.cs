using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Rules
{
    public class RuleResult
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string RuleName { get; set; }
        public decimal Premium { get; set; }
    }
}
