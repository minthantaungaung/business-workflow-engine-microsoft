using qal.waiter.domain.Data.DTO.Document;
using System.Text.RegularExpressions;

namespace qal.workflow.rule.engine.Helper
{
    public static class ReHelper
    {
        public static bool CheckContains(string item, List<string> listToCheck)
        {
            if (listToCheck.Contains(item)) return true;
            else return false;
        }
        public static bool CheckNotContains(string item, List<string> listToCheck)
        {
            if (listToCheck.Contains(item)) return false;
            else return true;
        }
        public static int ExtractOnlyNumber(string GPS)
        {
            if (string.IsNullOrEmpty(GPS)) return 0;
            var resultString = Regex.Match(GPS, @"\d+").Value;
            if (string.IsNullOrEmpty(resultString)) return 0;
            return int.Parse(resultString);
        }
    }

}
