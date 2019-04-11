using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.ServiceLayer.ErrorObject
{
    public class DtoWrapper<T>
    {
        public T Dto { get; set; }
        public bool HasError { get; set; }
        public string ErrorType { get; set; }
        public string ErrorMsg { get; set; }

        public DtoWrapper(string t, string msg) { ErrorType = t; ErrorMsg = msg; }
        public DtoWrapper(string msg) { ErrorType = "not specified"; ErrorMsg = msg; }
        public DtoWrapper() { ErrorType = "not specified"; ErrorMsg = "unknown Error"; }
    }
}