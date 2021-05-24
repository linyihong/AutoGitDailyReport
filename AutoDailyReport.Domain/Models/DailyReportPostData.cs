using System;

namespace AutoDailyReport.Domain.Models
{
    public class DailyReportPostData : PostData
    {
        public new DateTime StartDate => DateTime.Now.Date;
        public new DateTime EndDate => DateTime.Now.Date;
    }
}
