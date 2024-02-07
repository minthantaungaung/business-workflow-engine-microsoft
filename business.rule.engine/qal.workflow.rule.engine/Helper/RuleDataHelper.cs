using Newtonsoft.Json.Linq;
using qal.workflow.rule.engine.Dto.Constants;
using System.Text.Json;

namespace qal.workflow.rule.engine.Helper
{
    public class RuleDataHelper
    {
        public (string RuleSchema, List<List<string>> ListToCheck) GetRuleSchemaAndListToCheck(string RuleName)
        {
            //// Search Rule in config.json
            //string appsetting = File.ReadAllText(Path.Combine("workflow-config.json"));
            //JsonDocument appJson = JsonDocument.Parse(appsetting);
            //JsonElement Config = appJson.RootElement.GetProperty("WorkFlowSettings");


            //// Search Rule in rules.json
            //string fileData = File.ReadAllText(Path.Combine("rule.json"));
            //JsonDocument jsonDocument = JsonDocument.Parse(fileData);
            //JsonElement RuleSection = jsonDocument.RootElement
            //    .GetProperty("RuleCollection")
            //    .GetProperty(RuleName);
            //string RuleSchema = RuleSection.ToString();

            //// Search List To Check 
            //List<List<string>> ListToCheck = new();
            //var workFlowSettings = Config.Deserialize<List<RuleSetting>>() ?? new();
            //var currentRuleSetting = workFlowSettings.Where(x => x.RuleName == RuleName).SingleOrDefault();

            //if (currentRuleSetting?.ListToCheck != null && currentRuleSetting?.ListToCheck?.Count() != 0)
            //    foreach (var i in currentRuleSetting!.ListToCheck)
            //    {
            //        var cardSection = appJson.RootElement.GetProperty($"ListToCheckSettings:{i}");
            //        var list = cardSection.Deserialize<List<string>>() ?? new();
            //        ListToCheck.Add(list);
            //    }

            //return (ruleschema, listtocheck);
            return (default, default);
        }
    }
}
