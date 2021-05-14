using System;

namespace LBON.EntityFrameworkCore.Entities
{
    public interface IModificationAudited<TUser> : IHasLastModifier<TUser>, IHasLastModificationTime
    {
    }

    public interface IHasLastModifier<TUser>
    {
        TUser LastModifierId { get; set; }
    }

    public interface IHasLastModificationTime
    {
        DateTime? LastModificationTime { get; set; }
    }
}
