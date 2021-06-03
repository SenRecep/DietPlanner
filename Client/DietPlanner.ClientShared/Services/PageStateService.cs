
using DietPlanner.ClientShared.Services.Interfaces;

namespace DietPlanner.ClientShared.Services
{
    public class PageStateService : IPageStateService
    {
        private string url;
        public bool IsLoading { get; set; }
        public string Title { get; set; }
        public string PreviousUrl { get; set; }
        public string Url
        {
            get { return url; }
            set
            {
                PreviousUrl = url;
                url = value;
            }
        }

    }
}
