using Domain.Common.Interfaces;

namespace Domain.Common
{
    public abstract record FullEntity : AudiTableEntity, IDeletableEntity
    {
        public bool IsDeleted { get; set; } = false;
    }
}
