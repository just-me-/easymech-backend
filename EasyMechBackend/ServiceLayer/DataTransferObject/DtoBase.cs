using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.ServiceLayer.DataTransferObject
{
    public class DtoBase
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorId { get; set; }
    }
}
