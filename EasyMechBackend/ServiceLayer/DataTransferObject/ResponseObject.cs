﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.ServiceLayer.DataTransferObject
{
    public class ResponseObject<T>
        where T: class
    {

        public static readonly string OKTAG = "ok";
        public static readonly string ERRORTAG = "error";

        public T Data { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }


        //Regular case: Data provided, no message
        public ResponseObject(T data)
        {
            Data = data;
            Status = OKTAG;
            Message = "";
        }

        //Error case: no Data + Message
        public ResponseObject(string msg)
        {
            Data = null;
            Status = ERRORTAG;
            Message = msg;
        }

        //Custom Case: All Props manually set
        public ResponseObject(T data, string status, string msg)
        {
            Data = data;
            Status = status;
            Message = msg;
        }


    }
}