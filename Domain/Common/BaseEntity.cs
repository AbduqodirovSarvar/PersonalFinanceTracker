using Domain.Common.Interfaces;

namespace Domain.Common
{
    public abstract record BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
