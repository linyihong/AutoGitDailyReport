using AutoDailyReport.Domain.Models;

namespace AutoDailyReport.Domain
{
    public interface ISendService
    {
        void Send(DailyReportPostData postData);
    }
}
