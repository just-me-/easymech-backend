using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMechBackend.DataAccessLayer
{
    [NotMapped]
    public abstract class EntityWithValidate
    {

        //Todo Interface IValidatable und Validate() implemenbtierung offen lassen. ist ja lächerlich so. [vgl reservationen, einfach das eine clip in validate rein und gut is]
        public virtual void Validate()
        {
            ClipProps();
            FillRequiredProps();
        }

        protected abstract void FillRequiredProps();

        protected abstract void ClipProps();
    }
}
