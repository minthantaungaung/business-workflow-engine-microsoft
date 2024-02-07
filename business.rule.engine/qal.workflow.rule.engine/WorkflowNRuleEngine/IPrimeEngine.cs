using qal.waiter.domain.Data.DTO.Document;
using qal.workflow.rule.engine.Dto;

namespace qal.workflow.rule.engine.RuleEngine
{
    public interface IPrimeEngine
    {
        Task<RuleResponseDto?> RuleCheck(string RuleJsonSchema, object Data);
        Task<WorkflowResponseDto> ProcessDocumentWorkFlowCheck(string WorkFlowName, ProcessDocumentRequestDto Data);
    }
}
