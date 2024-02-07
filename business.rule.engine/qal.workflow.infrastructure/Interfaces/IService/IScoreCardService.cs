using qal.waiter.domain.Data.DTO.Document;
using qal.workflow.domain.Data.DTO.RuleDTOs;
using qal.workflow.domain.Data.DTO.ScoreCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qal.workflow.infrastructure.Interfaces.IService
{
    public interface IScoreCardService
    {
        Task<ScoreCardResponse?> GetScores(ScoreCardRuleRequest dto);
        Task<ScoreCardResponse?> VerifyCashLimitByScoreGrade(double totalScore);
    }
}
