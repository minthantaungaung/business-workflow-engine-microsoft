using qal.waiter.domain.Data.DTO.Document;
using qal.workflow.domain.Data.DTO.RuleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qal.workflow.infrastructure.Interfaces.IService
{
    public interface IRuleService
    {
        Task<ProcessDocumentResponseDto> processDocument(ProcessDocumentRequestDto dto);
        Task<AssigneeResponse> GetAssignee(OperatorAssignDto dto);
    }
}
