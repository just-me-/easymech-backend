

namespace EasyMechBackend.ServiceLayer
{
    public enum ErrorCode
    {
        General = 100,
        DBUpdate = 101,

        Uniqueness = 200,
        ForeignKey = 201,
        IDMismatch = 202
    }
}
