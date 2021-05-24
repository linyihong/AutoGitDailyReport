using AutoDailyReport.Domain.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace AutoDailyReport.Domain
{
    public class SendService : ISendService
    {
        public void Send(DailyReportPostData postData)
        {
            if (postData.IsNoData)
            {
                return;
            }

            const string postUrl = "postUrl";

            var request = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });

            var formContent = new FormUrlEncodedContent(
                new[]
                {
                    new KeyValuePair<string, string>("PK_ID", postData.ID.ToString()),
                    new KeyValuePair<string, string>(nameof(DailyReportPostData.EmpID), postData.EmpID),
                    new KeyValuePair<string, string>(nameof(DailyReportPostData.DomainID), postData.DomainID),
                    new KeyValuePair<string, string>(nameof(DailyReportPostData.StartDate), postData.StartDate.ToString("yyyy-MM-dd")),
                    new KeyValuePair<string, string>(nameof(DailyReportPostData.EndDate), postData.EndDate.ToString("yyyy-MM-dd")),
                    new KeyValuePair<string, string>(nameof(DailyReportPostData.NumberOfWeek), postData.NumberOfWeek.ToString()),
                    new KeyValuePair<string, string>(nameof(DailyReportPostData.CreateDate), postData.CreateDate.ToString("yyyy-MM-dd")),
                    new KeyValuePair<string, string>(nameof(DailyReportPostData.Content), Uri.EscapeUriString(postData.Content)),
                });

            using (var response = request.PostAsync(postUrl, formContent).Result)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var bodyContent = response.Content.ReadAsStringAsync().Result;

                    if (bodyContent != "\"ok\"")
                    {
                        MessageBox.Show("Have problem!Check It!");
                    }
                }
                else
                {
                    MessageBox.Show("You have to see AutoDailyReport Programs!");
                }
            }
        }
    }
}
