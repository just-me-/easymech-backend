using EasyMechBackend.DataAccessLayer;

namespace EasyMechBackend.BusinessLayer
{
    public class ManagerBase
    {
        public EMContext Context { get; set; }

        public ManagerBase(EMContext context)
        {
            Context = context;
        }


        public ManagerBase()
        {
            Context = new EMContext();
        }



    }
}