using System;

namespace LBON.EntityFrameworkCore.Entities
{
    public interface ICreationAudited<TUser> : IHasCreator<TUser>, IHasCreationTime
    {
    }

    public interface IHasCreator<TUser>
    {
        TUser CreatorId { get; set; }
    }

    public interface IHasCreationTime
    {
        DateTime CreationTime { get; set; }
    }
}
