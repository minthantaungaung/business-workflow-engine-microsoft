using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qal.workflow.domain.Data.DTO
{

    public class BaseRespModel<T> : Property where T : class
    {
        public string? KBZRefNo { get; set; }
        public T? Data { get; set; }
        public BaseResp? Error { get; set; }
    }

    public class BaseResp
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public List<BaseRespDetail>? Details { get; set; } = new List<BaseRespDetail>();
    }

    public class BaseRespDetail
    {
        public string? ErrorCode { get; set; }
        public string? ErrorDescription { get; set; }
    }

    public class BaseData
    {
        public bool IsApiSuccess { get; set; }
    }

    public class Property
    {
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
    public static class ErrorCode
    {
        public static BaseResp Unauthorized
        { get { return new BaseResp { Code = "1000", Message = "Unauthorized." }; } }

        public static BaseResp ValidationError
        { get { return new BaseResp { Code = "1001", Message = "Validation error." }; } }

        public static BaseResp Timeout
        { get { return new BaseResp { Code = "1002", Message = "Timeout error." }; } }

        public static BaseResp UnknownException
        { get { return new BaseResp { Code = "1004", Message = "Indicate unknown exception." }; } }

        public static BaseResp UnknownError
        { get { return new BaseResp { Code = "1004", Message = "Unknown error" }; } }

        public static BaseResp Maintenance
        { get { return new BaseResp { Code = "1005", Message = "Service is in Maintenance mode." }; } }

        public static BaseResp Duplicate
        { get { return new BaseResp { Code = "1011", Message = "Duplicate Data" }; } }

        public static BaseResp NoRowsAffected
        { get { return new BaseResp { Code = "1012", Message = "No Rows Affected." }; } }

        public static BaseResp NoRecordFound
        { get { return new BaseResp { Code = "1013", Message = "No Record Found." }; } }

        public static BaseResp DatabaseError
        { get { return new BaseResp { Code = "1015", Message = "Database Error." }; } }

        public static BaseResp AlreadyExit
        { get { return new BaseResp { Code = "1001", Message = "Already existed." }; } }
        public static BaseRespDetail InvalidRequestPayload
        { get { return new BaseRespDetail { ErrorCode = "103000001", ErrorDescription = "Requested payload is Invalid." }; } }

        public static BaseRespDetail OperationError
        { get { return new BaseRespDetail { ErrorCode = "103000002", ErrorDescription = "Indicate unknown exception." }; } }

        public static BaseRespDetail OperationErrorBiz
        { get { return new BaseRespDetail { ErrorCode = "103000002", ErrorDescription = "Indicate unknown exception." }; } }

        public static BaseRespDetail InvalidRequestParameter
        { get { return new BaseRespDetail { ErrorCode = "103000020", ErrorDescription = "Mandatory field not found." }; } }

        public static BaseRespDetail ThridParty_ErrorDetail
        { get { return new BaseRespDetail { ErrorCode = "103000021", ErrorDescription = "{0}" }; } }

        public static BaseRespDetail CustomErrorMsg
        { get { return new BaseRespDetail { ErrorCode = "103000022", ErrorDescription = "{0}" }; } }

    }

}
