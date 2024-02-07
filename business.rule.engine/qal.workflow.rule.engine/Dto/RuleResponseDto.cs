using qal.workflow.domain.Data.DTO;
using RulesEngine.Models;

namespace qal.workflow.rule.engine.Dto
{
    public class RuleResponseDto : Property
    {
        public bool? ResultStatus { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessActionMessage { get; set; }
        public List<RuleResultTree>? Details { get; set; }
    }

    public class CardActivateRuleResponseDto : RuleResponseDto
    {
        public string? CardStatus { get; set; }
    }
}
