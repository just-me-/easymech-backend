using System.ComponentModel.DataAnnotations.Schema;

namespace EasyMechBackend.DataAccessLayer
{
    public interface IValidatable
    {
        void Validate();
    }
}
