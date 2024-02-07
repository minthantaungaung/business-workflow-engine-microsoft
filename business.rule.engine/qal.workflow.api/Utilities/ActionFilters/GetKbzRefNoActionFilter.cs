using Microsoft.AspNetCore.Mvc.Filters;

namespace qal.workflow.api.Utilities.ActionFilters
{
    public class GetKbzRefNoActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string kbzRefNo = context.HttpContext.Request.Headers.TryGetValue("KBZ_REF_NO", out var refno) ? refno : context.HttpContext.Request.Headers["LOGID"];
            context.HttpContext.Items.Add("KBZ_REF_NO", kbzRefNo);
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
