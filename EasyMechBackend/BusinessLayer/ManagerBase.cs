using EasyMechBackend.DataAccessLayer;
using System;

namespace EasyMechBackend.BusinessLayer
{
    public class ManagerBase : IDisposable
    {
        public EMContext Context { get; set; }

        public void Dispose()
        {
            Context.Dispose();
            Context = null;
        }
    }
}