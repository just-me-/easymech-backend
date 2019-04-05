using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.ServiceLayer.ErrorObject
{
    public class ErrorDto
    {
        public string ErrorType { get; set; }
        public string ErrorMsg { get; set; }

        public ErrorDto(string t, string msg) { ErrorType = t; ErrorMsg = msg; }
        public ErrorDto(string msg) { ErrorType = "not specified"; ErrorMsg = msg; }
        public ErrorDto() { ErrorType = "not specified"; ErrorMsg = "unknown Error"; }
    }
}