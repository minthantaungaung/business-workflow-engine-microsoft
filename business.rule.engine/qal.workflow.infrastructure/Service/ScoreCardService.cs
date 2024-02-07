using Newtonsoft.Json;
using qal.waiter.domain.Data.DTO.Document;
using qal.workflow.domain.Data.DTO.RuleDTOs;
using qal.workflow.domain.Data.DTO.ScoreCard;
using qal.workflow.domain.Interfaces.IService;
using qal.workflow.infrastructure.Data.DTO.RuleEngine;
using qal.workflow.infrastructure.Interfaces.IService;
using qal.workflow.rule.engine.Helper;
using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace qal.workflow.infrastructure.Service
{
    public class ScoreCardService : IScoreCardService
    {
        private readonly ILoggerManager _logger;
        public ScoreCardService(ILoggerManager logger)
        {
            _logger = logger;
        }

        public async Task<ScoreCardResponse?> GetScores(ScoreCardRuleRequest dto)
        {
            _logger.LogInfo($"Score Card request : {dto}");
            var scoreResponse = await VerifyScoreCardRules("LoanFormCreditScoreRule", dto);
            _logger.LogInfo($"Score Card response : {scoreResponse}");
            return scoreResponse;
        }
        public async Task<ScoreCardResponse?> VerifyCashLimitByScoreGrade(double totalScore)
        {
            var data = new { ScoreGrade = totalScore };
            _logger.LogInfo($"Cash Limit request : {totalScore}");
            var rule = await VerifyScoreCardRules("CreditScoreCashLimitRule", data);
            _logger.LogInfo($"Cash Limit response : {rule}");
            return rule;
        }

        #region Private Methods
        public async Task<ScoreCardResponse?> VerifyScoreCardRules(string RuleName, dynamic Data)
        {
            ScoreCardResponse response = new();
            List<ScoreDto> scoreDetail = new();
            
            var DataHelper = new WorkflowDataHelper();
            var SchemaData = DataHelper.GetWorkflowSchemaAndListToCheck(RuleName, "Utilities/Data/scorecard.json");

            if (string.IsNullOrEmpty(SchemaData.RuleSchema)) return default;

            try
            {
                List<RuleResultTree> resultList = new();

                string ErrorMessage = string.Empty;
                var Rulelist = JsonConvert.DeserializeObject<List<Workflow>>(SchemaData.RuleSchema);

                ReSettings reSetting = new ReSettings { CustomTypes = new Type[] { typeof(ReHelper) } };
                var engine = new RulesEngine.RulesEngine(Rulelist!.ToArray(), reSetting);
                var input = new RuleParameter("input", Data);

                foreach (var i in Rulelist)
                {
                    bool ResultStatus = false;

                    var data = await engine.ExecuteAllRulesAsync(i.WorkflowName, input);
                    resultList.AddRange(data);

                    RuleResultTree? successData = data.Where(item => item.IsSuccess == true).FirstOrDefault();
                    if (successData is not null)
                        ResultStatus = successData!.IsSuccess;
                    //TODO : PROPER ERROR RETURN

                    _logger.LogInfo($"Rule : {i.WorkflowName} , IsSuccess : {ResultStatus}");

                    if (!ResultStatus)
                    {
                        var errorMsg = data.Where(rule => !rule.IsSuccess && !string.IsNullOrEmpty(rule.Rule.ErrorMessage))
                            .Select(rule => rule.Rule.ErrorMessage).Distinct().ToList();

                        ErrorMessage = string.Join(", ", errorMsg);
                        scoreDetail.Add(new ScoreDto() { ErrorMessage = ErrorMessage });
                        continue;
                    }
                    else
                    {
                        scoreDetail.Add(new ScoreDto()
                        {
                            Score = successData!.Rule.SuccessEvent,
                            Name = i.WorkflowName,
                            Details = resultList
                        });
                    }
                }
                response.ScoreDetail.AddRange(scoreDetail);
                response.TotalScore = scoreDetail.Where(x => !string.IsNullOrEmpty(x.Score))
                                    .Select(x => double.TryParse(x.Score, out var scoreValue) ? scoreValue : 0).Sum();
                _logger.LogInfo($"Score Response : {response}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} => Exception occurred while valdating rule");
                _logger.LogError($"Raw Exception =============> \n {ex}");
                return null;
            }
            return response;
        }
        #endregion
    }
}
