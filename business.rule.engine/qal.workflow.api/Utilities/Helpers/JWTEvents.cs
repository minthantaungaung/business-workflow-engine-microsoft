using Microsoft.AspNetCore.Authentication.JwtBearer;
using qal.workflow.domain.Data.DTO;

namespace qal.workflow.api.Utilities.Helpers
{
    public class JWTEvents : JwtBearerEvents
    {
        public JWTEvents()
        {
            OnChallenge = async context =>
            {
                string KBZRefNo = context.HttpContext.Request.Headers.TryGetValue("KBZ_REF_NO", out var refno) ? refno : context.HttpContext.Request.Headers["LOGID"];
                BaseRespModel<object> response = new()
                {
                    KBZRefNo = KBZRefNo,
                    Error = new BaseResp
                    {
                        Code = ErrorCode.Unauthorized.Code,
                        Message = ErrorCode.Unauthorized.Message,
                        Details = new List<BaseRespDetail> {
                        new BaseRespDetail
                        {
                            ErrorCode = "AUTH",
                            ErrorDescription = !string.IsNullOrEmpty(context.ErrorDescription) ? context.ErrorDescription : "Invalid token"
                        }
                    }
                    }
                };
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response.ToString());
            };
        }
    }
}
