namespace DietPlanner.Server.Entities.Interfaces
{
    public interface IDiet : IEntityBase, IReportList
    {
        string Name { get; set; }
        string Description { get; set; }
    }
}
