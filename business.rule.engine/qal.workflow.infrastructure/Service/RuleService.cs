using Newtonsoft.Json;
using qal.waiter.domain.Data.DTO.Document;
using qal.workflow.domain.Data.DTO.RuleDTOs;
using qal.workflow.domain.Interfaces.IService;
using qal.workflow.infrastructure.Data.DTO.Document;
using qal.workflow.infrastructure.Interfaces.IService;
using qal.workflow.rule.engine.Helper;
using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qal.workflow.infrastructure.Service
{
    public class RuleService : IRuleService
    {
        private readonly ILoggerManager _logger;
        public RuleService(ILoggerManager logger)
        {
            _logger = logger;
        }

        public async Task<ProcessDocumentResponseDto> processDocument(ProcessDocumentRequestDto dto)
        {
            var data = await ProcessDocumentWorkFlowCheck("ProcessDocument", dto);
            ProcessDocumentResponseDto process = new()
            {
                IsSuccess = true,
                Receiver = data.Reciever,
                Message = data.ErrorMessage
            };
            return process;
        }
        public async Task<AssigneeResponse?> GetAssignee(OperatorAssignDto dto)
        {
            var assignee = dto.Assignees!
                .OrderBy(x => x.Cases)
                .ThenBy(x => x.EmployeeId)
                .FirstOrDefault();

            return new()
            {
                DocumentNo = dto.DocumentNo,
                Cases = assignee!.Cases,
                EmployeeId = assignee.EmployeeId,
                Name = assignee.Name
            };
        }

        #region Private Methods
        public async Task<ProcessLoanWorkflowResponseDto> ProcessDocumentWorkFlowCheck(string WorkFlowName, ProcessDocumentRequestDto Data)
        {
            ProcessLoanWorkflowResponseDto response = new();
            var ruleDataHelper = new WorkflowDataHelper();
            var SchemaData = ruleDataHelper.GetWorkflowSchemaAndListToCheck(WorkFlowName, "Utilities/Data/workflow.json");

            if (string.IsNullOrEmpty(SchemaData.RuleSchema)) return response;
            try
            {

                List<RuleResultTree> resultList = new();
                bool ResultStatus = false;
                string ErrorMessage = string.Empty;

                var workflowlist = JsonConvert.DeserializeObject<List<Workflow>>(SchemaData.RuleSchema);

                ReSettings reSetting = new ReSettings { CustomTypes = new Type[] { typeof(ReHelper) } };
                var engine = new RulesEngine.RulesEngine(workflowlist!.ToArray(), reSetting);

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
                        response = new ProcessLoanWorkflowResponseDto()
                        {
                            ResultStatus = ResultStatus,
                            Reciever = successData!.Rule.SuccessEvent,
                            Details = resultList
                        };
                        break;
                    }

                    // var errorList = data.Select(x => x.ExceptionMessage);
                    response = new ProcessLoanWorkflowResponseDto()
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
        #endregion
    }
}
