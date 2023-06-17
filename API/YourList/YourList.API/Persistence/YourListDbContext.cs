using YourList.API.Models;

namespace YourList.API.Persistence
{
    public class YourListDbContext
    {
        public List<DailyTasks> DailyTasks { get; set; }
        public YourListDbContext()
        {
            DailyTasks = new List<DailyTasks>();
        }

    }
}
