using System;
using System.Security.Principal;

namespace AutoDailyReport.Domain.Models
{
    public class PostData
    {
        public Guid ID
        {
            get
            {
                if (id == Guid.Empty)
                    id = Guid.NewGuid();
                return id;
            }
        }
        public string EmpID { get; set; }
        public string DomainID => WindowsIdentity.GetCurrent().Name;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfWeek => (int)Math.Ceiling((decimal)(DateTime.Now.Subtract(new DateTime(DateTime.Now.Year, 1, 1)).TotalDays / 7)) + 1;
        public DateTime CreateDate { get; set; }
        public string Content
        {
            get
            {
                return "<pre>\n" + _content + "\n</pre>\n";
            }
            set
            {
                _content = value;
            }
        }

        public bool IsNoData { get; set; }

        private Guid id;

        private string _content;

    }
}
