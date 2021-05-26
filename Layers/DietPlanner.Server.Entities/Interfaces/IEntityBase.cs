using System;

namespace DietPlanner.Server.Entities.Interfaces
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
        DateTime CreatedTime { get; set; }
        DateTime? UpdateTime { get; set; }
        Guid CreateUserId { get; set; }
        Guid? UpdateUserId { get; set; }
        bool IsDeleted { get; set; }
    }
}
