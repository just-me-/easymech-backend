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
        MaintenanceException = 204,

        Unknown = 400
    }
}
