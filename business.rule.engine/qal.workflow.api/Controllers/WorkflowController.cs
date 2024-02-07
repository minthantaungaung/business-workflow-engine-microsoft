using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using qal.waiter.domain.Data.DTO.Document;
using qal.workflow.api.Utilities.ActionFilters;
using qal.workflow.domain.Data.DTO;
using qal.workflow.domain.Data.DTO.RuleDTOs;
using qal.workflow.domain.Interfaces.IService;
using qal.workflow.infrastructure.Interfaces.IService;

namespace qal.workflow.api.Controllers
{
    //[Authorize]
    [Route("api/workflow/")]
    [ApiController]
    [ServiceFilter(typeof(GetKbzRefNoActionFilter))]
    public class WorkflowController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRuleService _service;


        public WorkflowController(IRuleService service, ILoggerManager logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("processDocument", Name = "processDocument")]
        public async Task<IActionResult> processDocument(ProcessDocumentRequestDto dto)
        {
            string? KbzRefNo = HttpContext.Items["KBZ_REF_NO"] as string;
            string actionName = ControllerContext.ActionDescriptor.ActionName;
            BaseRespModel<ProcessDocumentResponseDto> response = new();
            response.KBZRefNo = KbzRefNo;

            _logger.LogRequest(KbzRefNo, actionName, dto);

            var data = await _service.processDocument(dto);
            response.Data = data;

            _logger.LogResponse(KbzRefNo,actionName, response);

            return Ok(response);
        }

        [HttpPost("OperatorAssign", Name = "OperatorAssign")]
        public async Task<IActionResult> OperatorAssign(OperatorAssignDto dto)
        {
            string? KbzRefNo = HttpContext.Items["KBZ_REF_NO"] as string;
            string actionName = ControllerContext.ActionDescriptor.ActionName;
            BaseRespModel<AssigneeResponse> response = new();
            response.KBZRefNo = KbzRefNo;

            _logger.LogRequest(KbzRefNo, actionName, dto);

            var data = await _service.GetAssignee(dto);
            response.Data = data;

            _logger.LogResponse(KbzRefNo, actionName, response);

            return Ok(response);
        }
    }
}
