namespace SharedKernel.Domain
{
    public interface IBaseEntity<TKey> : ICoreEntity, IAuditable, ICloneable
    {
        TKey Id { get; set; }

        bool IsDeleted { get; set; }
    }

    public interface IBaseEntity : IBaseEntity<Guid> { }
}
