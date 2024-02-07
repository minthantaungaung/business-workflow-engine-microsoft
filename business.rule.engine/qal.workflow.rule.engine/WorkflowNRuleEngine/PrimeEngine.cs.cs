using Newtonsoft.Json;
using qal.waiter.domain.Data.DTO.Document;
using qal.workflow.domain.Data.Constants.Appsettings;
using qal.workflow.domain.Interfaces.IService;
using qal.workflow.rule.engine.Dto;
using qal.workflow.rule.engine.Helper;
using RulesEngine.Extensions;
using RulesEngine.HelperFunctions;
using RulesEngine.Models;

namespace qal.workflow.rule.engine.RuleEngine
{
    public class PrimeEngine : IPrimeEngine
    {
        private readonly ILoggerManager _logger;
        public PrimeEngine(ILoggerManager log)
        {
            _logger = log;
        }
        public async Task<RuleResponseDto?> RuleCheck(string RuleName, object Data)
        {
            RuleResponseDto response = new();
            var ruleDataHelper = new RuleDataHelper();
            var SchemaData = ruleDataHelper.GetRuleSchemaAndListToCheck(RuleName);

            if (string.IsNullOrEmpty(SchemaData.RuleSchema)) return response;

            try
            {
                List<RuleResultTree> resultList = new();
                bool ResultStatus = false;
                string ErrorMessage = string.Empty;
                string SuccessEvent = string.Empty;

                var workflowlist = JsonConvert.DeserializeObject<List<Workflow>>(SchemaData.RuleSchema);

                ReSettings reSetting = new ReSettings { CustomTypes = new Type[] { typeof(ReHelper) } };
                var engine = new RulesEngine.RulesEngine(workflowlist!.ToArray(), reSetting);
                var input = new RuleParameter("input", Data);

                foreach (var i in workflowlist)
                {
                    var data = await engine.ExecuteAllRulesAsync(i.WorkflowName, input);
                    resultList.AddRange(data);
                    ResultStatus = data[0].IsSuccess;

                    _logger.LogInfo($"Rule : {i.WorkflowName} , IsSuccess : {ResultStatus}");

                    if (!data[0].IsSuccess)
                    {
                        ErrorMessage += data[0].Rule.ErrorMessage;
                        continue;
                    }
                    else
                    {
                        SuccessEvent += data[0].Rule.SuccessEvent;
                    }
                }
                response = new RuleResponseDto()
                {
                    ResultStatus = ResultStatus,
                    ErrorMessage = ErrorMessage,
                    SuccessActionMessage = SuccessEvent,
                    Details = resultList
                };

            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} => Exception occurred while valdating rule");
                _logger.LogError($"Raw Exception =============> \n {ex}");
                return null;
            }
            return response;
        }
        public async Task<WorkflowResponseDto> ProcessDocumentWorkFlowCheck(string WorkFlowName, ProcessDocumentRequestDto Data)
        {
            WorkflowResponseDto response = new();
            var ruleDataHelper = new WorkflowDataHelper();
            var SchemaData = ruleDataHelper.GetWorkflowSchemaAndListToCheck(WorkFlowName);

            if (string.IsNullOrEmpty(SchemaData.RuleSchema)) return response;
            try
            {

                List<RuleResultTree> resultList = new();
                bool ResultStatus = false;
                string ErrorMessage = string.Empty;

                var workflowlist = JsonConvert.DeserializeObject<List<Workflow>>(SchemaData.RuleSchema);

                ReSettings reSetting = new ReSettings { CustomTypes = new Type[] { typeof(ReHelper) } };
                var engine = new RulesEngine.RulesEngine(workflowlist!.ToArray(), reSetting);
                //var input = new RuleParameter("input", Data);
                Data.ListToCheck = SchemaData.ListToCheck;
                var input = new RuleParameter("input", Data);
                foreach (var i in workflowlist)
                {
                    var data = await engine.ExecuteAllRulesAsync(i.WorkflowName, input);
                    resultList.AddRange(data);
                    var successData = data.Where(item => item.IsSuccess == true).FirstOrDefault();
                    ResultStatus = data.Any(item => item.IsSuccess == true);

                    _logger.LogInfo($"Rule : {i.WorkflowName} , IsSuccess : {ResultStatus}");

                    if (ResultStatus)
                    {
                        response = new WorkflowResponseDto()
                        {
                            ResultStatus = ResultStatus,
                            Reciever = successData!.Rule.SuccessEvent,
                            Details = resultList
                        };
                        break;
                    }
                    
                   // var errorList = data.Select(x => x.ExceptionMessage);
                    response = new WorkflowResponseDto()
                    {
                        ResultStatus = ResultStatus,
                        ErrorMessage = string.Join(", ", data.Select(x => x.ExceptionMessage)),
                        Details = resultList
                    };
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} => Exception occurred while valdating rule");
                _logger.LogError($"Raw Exception =============> \n {ex}");
                return null;
            }
            return response;
        }
    }
}
