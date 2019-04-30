using EasyMechBackend.DataAccessLayer;
using System;

namespace EasyMechBackend.BusinessLayer
{
    public class ManagerBase
    {
        public EMContext Context { get; set; }
    }
}