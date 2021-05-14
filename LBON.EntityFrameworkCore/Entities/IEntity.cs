namespace LBON.EntityFrameworkCore.Entities
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
