namespace Domain.Utilities.Enums
{
    public enum State
    {
        NotDeleted,
        Deleted
    }
    public enum Status
    {
        IsDrafft = 1,
        IsPublish = 2,
    }
    public enum OfferType
    {
        Used = 1,
        Like = 2
    }

    public enum StatusEnum
    {
        FailedToSave,
        SavedSuccessfully,
        Exception,
        Success,
        Failed,
        FailedToFindTheObject,
        AlreadyExisting
    }


    public enum EnumStudy
    {
        Draft,
        InPath,
        Rejected,
        Approved
    }
    public enum EnumSession
    {

        Draft,
        InPath,
        Rejected,
        Approved,
        Finished,
        Sent
    }






}