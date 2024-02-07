using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using qal.waiter.domain.Data.DTO.Document;
using qal.workflow.api.Utilities.ActionFilters;
using qal.workflow.domain.Data.DTO;
using qal.workflow.domain.Data.DTO.RuleDTOs;
using qal.workflow.domain.Data.DTO.ScoreCard;
using qal.workflow.domain.Interfaces.IService;
using qal.workflow.infrastructure.Interfaces.IService;

namespace qal.workflow.api.Controllers
{
    //[Authorize]
    [Route("api/rule/verify/")]
    [ApiController]
    [ServiceFilter(typeof(GetKbzRefNoActionFilter))]
    public class ScoreCardController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IScoreCardService _service;
        

        public ScoreCardController(IScoreCardService service, ILoggerManager logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("scorecard", Name = "ScoreCard")]
        public async Task<IActionResult> ScoreCard(ScoreCardRuleRequest dto)
        {
            string? KbzRefNo = HttpContext.Items["KBZ_REF_NO"] as string;
            string actionName = nameof(ScoreCard);

            _logger.LogRequest(KbzRefNo, actionName, dto);

            BaseRespModel<ScoreCardResponse> response = new() { KBZRefNo = KbzRefNo};
            var data = await _service.GetScores(dto);

            if (data == null)
                response.Error = ErrorCode.DatabaseError;

            response.Data = data;
            _logger.LogResponse(KbzRefNo, actionName, response);
            return Ok(response);
        }

        [HttpGet("cashLimit", Name = "VerifyCashLimit")]
        public async Task<IActionResult> VerifyCashLimit(double score)
        {
            string? KbzRefNo = HttpContext.Items["KBZ_REF_NO"] as string;
            string actionName = nameof(VerifyCashLimit);

            _logger.LogRequest(KbzRefNo, actionName, score);
            
            BaseRespModel<ScoreCardResponse> response = new() { KBZRefNo = KbzRefNo };

            var data = await _service.VerifyCashLimitByScoreGrade(score);
            if (data == null)
                response.Error = ErrorCode.DatabaseError;
            response.Data = data;

            _logger.LogResponse(KbzRefNo, actionName, response);
            return Ok(response);
        }
    }
}
