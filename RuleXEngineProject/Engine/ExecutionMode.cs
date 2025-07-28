using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Engine
{
    public enum ExecutionMode
    {
        /// <summary>
        /// Execute all rules regardless of failures
        /// </summary>
        AllPass,
        
        /// <summary>
        /// Stop execution on first rule failure
        /// </summary>
        FirstFail,
        
        /// <summary>
        /// Execute all rules and calculate a score based on success/failure ratio
        /// </summary>
        Scored
    }
}