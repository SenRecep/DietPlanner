namespace DietPlanner.Server.Entities.Interfaces
{
    public interface IDisease : IEntityBase, IReportList
    {
        string Name { get; set; }
    }
}
