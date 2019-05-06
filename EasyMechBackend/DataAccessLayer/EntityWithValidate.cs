using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMechBackend.DataAccessLayer
{
    [NotMapped]
    public abstract class EntityWithValidate
    {

        public virtual void Validate()
        {
            ClipProps();
            FillRequiredProps();
        }

        protected abstract void FillRequiredProps();

        protected abstract void ClipProps();
    }
}
