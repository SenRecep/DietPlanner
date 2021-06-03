namespace DietPlanner.ClientShared.Services.Interfaces
{
    public interface IPageStateService
    {
        bool IsLoading { get; set; }
        string Title { get; set; }
        string PreviousUrl { get; set; }
        string Url { get; set; }
    }
}
