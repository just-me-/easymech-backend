namespace EasyMechBackend.Common
{
    public enum ErrorCode
    {
        General = 100,
        DBUpdate = 101,

        Uniqueness = 200,
        ForeignKey = 201,
        IDMismatch = 202,
        ReservationException = 203,

        Unknown = 400
    }
}
