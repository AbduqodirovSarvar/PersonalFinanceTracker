using Domain.Common.Interfaces;

namespace Domain.Common
{
    public record DeletableEntity : IDeletableEntity
    {
        public bool IsDeleted { get; set; } = false;
    }
}
