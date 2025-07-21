namespace Domain.Common.Interfaces
{
    public interface IAudiTableEntity
    {
        DateTime CreatedAt { get; set; }
        Guid? CreatedBy { get; set; }
        DateTime? UpdatedAt { get; set; }
        Guid? UpdatedBy { get; set; }
    }
}
