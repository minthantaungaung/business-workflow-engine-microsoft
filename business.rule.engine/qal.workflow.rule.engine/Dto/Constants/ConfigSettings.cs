using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qal.workflow.rule.engine.Dto.Constants
{
    public class RuleSetting
    {
        public string? RuleName { get; set; }
        public List<string>? ListToCheck { get; set; }
    }

    public class WorkFlowSetting
    {
        public string? WorkFlowName { get; set; }
        public List<string>? ListToCheck { get; set; }
    }
}
