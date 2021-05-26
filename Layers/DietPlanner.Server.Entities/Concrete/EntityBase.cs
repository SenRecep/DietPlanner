using System;

using DietPlanner.Server.Entities.Interfaces;

namespace DietPlanner.Server.Entities.Concrete
{
    public class EntityBase : IEntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid CreateUserId { get; set; }
        public Guid? UpdateUserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
