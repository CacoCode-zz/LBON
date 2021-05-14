using System;

namespace LBON.EntityFrameworkCore.Entities
{
    public class FullAuditedEntity<TKey,TUser>:EntityBase<TKey>, ICreationAudited<TUser>, IModificationAudited<TUser>, IDeletionAudited<TUser>
    {
        public TUser CreatorId { get; set; }
        public DateTime CreationTime { get; set; }
        public TUser LastModifierId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public TUser DeleterId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
    }
}
