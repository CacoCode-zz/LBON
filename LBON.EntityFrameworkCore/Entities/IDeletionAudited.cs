using System;

namespace LBON.EntityFrameworkCore.Entities
{
    public interface IDeletionAudited<TUser> : IHasDeleter<TUser>,ISoftDelete, IHasDeletionTime
    {
    }

    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }

    public interface IHasDeleter<TUser>
    {
        TUser DeleterId { get; set; }
    }

    public interface IHasDeletionTime
    {
        DateTime? DeletionTime { get; set; }
    }
}
