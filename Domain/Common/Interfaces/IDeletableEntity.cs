namespace Domain.Common.Interfaces
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
    }
}
