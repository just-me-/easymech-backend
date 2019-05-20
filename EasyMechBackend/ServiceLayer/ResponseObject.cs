
using EasyMechBackend.Common;

namespace EasyMechBackend.ServiceLayer
{
    public class ResponseObject<T>
        where T: class
    {
        public const string OKTAG = "ok";
        public const string ERRORTAG = "error";

        public T Data { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }

        public ErrorCode ErrorCode { get; set; }

        //Regular case: Data provided, no message
        public ResponseObject(T data)
        {
            Data = data;
            Status = OKTAG;
            Message = "";
            ErrorCode = 0;
        }

        //Error case: no Data + Message
        public ResponseObject(string msg, ErrorCode errorCode)
        {
            Data = null;
            Status = ERRORTAG;
            Message = msg;
            ErrorCode = errorCode;
        }

        //Custom Case: All Props manually set
        public ResponseObject(T data, string status, string msg, ErrorCode errorCode)
        {
            Data = data;
            Status = status;
            Message = msg;
            ErrorCode = errorCode;
        }
    }
}