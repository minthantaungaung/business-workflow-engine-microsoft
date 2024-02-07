using Newtonsoft.Json.Linq;
using qal.workflow.rule.engine.Dto.Constants;
using System.Text.Json;

namespace qal.workflow.rule.engine.Helper
{
    public class WorkflowDataHelper
    {
        public (string RuleSchema, List<List<string>> ListToCheck) GetWorkflowSchemaAndListToCheck(string worflowName)
        {
            var s = System.IO.Directory.GetCurrentDirectory();
            // Search Rule in config.json
            string appsetting = File.ReadAllText(Path.Combine("appsettings.json"));
            JsonDocument appJson = JsonDocument.Parse(appsetting);
            JsonElement Config = appJson.RootElement.GetProperty("WorkFlowSettings");


            // Search Rule in rules.json
            string fileData = File.ReadAllText(Path.Combine("workflow.json"));
            JsonDocument jsonDocument = JsonDocument.Parse(fileData);
            JsonElement RuleSection = jsonDocument.RootElement
                .GetProperty("WorkFlowCollection")
                .GetProperty(worflowName);
            string RuleSchema = RuleSection.ToString();

            // Search List To Check 
            List<List<string>> ListToCheck = new();
            var workFlowSettings = Config.Deserialize<List<WorkFlowSetting>>() ?? new();
            var currentRuleSetting = workFlowSettings.Where(x => x.WorkFlowName == worflowName).SingleOrDefault();
            try
            {
                if (currentRuleSetting?.ListToCheck != null && currentRuleSetting?.ListToCheck?.Count() != 0)
                    foreach (var i in currentRuleSetting!.ListToCheck)
                    {
                        var cardSection1 = appJson.RootElement.GetProperty($"ListToCheckSettings");
                        var ltc = cardSection1.GetProperty(i);
                       // var cardSection = appJson.RootElement.GetProperty($"ListToCheckSettings:{i}");
                        var list = ltc.Deserialize<List<string>>() ?? new();
                        ListToCheck.Add(list);
                    }
            }
            catch (Exception ex)
            {

            }


            return (RuleSchema, ListToCheck);
        }
    }
}
