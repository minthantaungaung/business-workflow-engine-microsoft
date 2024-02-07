using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qal.workflow.rule.engine.Dto
{
    public class WorkflowResponseDto
    {
        public bool ResultStatus { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Reciever { get; set; }
        public List<RuleResultTree>? Details { get; set; }
    }
}
