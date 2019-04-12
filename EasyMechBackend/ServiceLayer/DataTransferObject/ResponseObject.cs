using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.ServiceLayer.DataTransferObject
{
    public class ResponseObject<T>
        where T: class
    {

        public T Data { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }


        public ResponseObject(T data)
        {
            Data = data;
            Status = "ok";
            Message = "";
        }

        public ResponseObject(string state, string msg)
        {
            Status = state;
            Message = msg;
            Data = null;
        }
    }
}