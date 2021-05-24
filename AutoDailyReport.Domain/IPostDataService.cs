using AutoDailyReport.Domain.Models;

namespace AutoDailyReport.Domain
{
    public interface IPostDataService
    {
        DailyReportPostData GetDailyPostData();
    }
}
