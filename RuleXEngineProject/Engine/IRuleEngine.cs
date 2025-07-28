using RuleXEngineProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Engine
{
    public interface IRuleEngine<T>
    {
        RuleEngineResult<T> Evaluate(T input);
        RuleEngineResult<T> Evaluate(T input, ExecutionMode mode);
    }
}