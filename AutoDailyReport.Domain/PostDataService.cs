using AutoDailyReport.Domain.Models;
using GitTools;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoDailyReport.Domain
{
    public class PostDataService : IPostDataService
    {
        private readonly IConfiguration configuration;
        private readonly IGitTool gitTool;

        public PostDataService(IConfiguration configuration, IGitTool gitTool)
        {
            this.configuration = configuration;
            this.gitTool = gitTool;
        }


        public DailyReportPostData GetDailyPostData()
        {
            var postData = new DailyReportPostData();

            postData.Content = configuration["DailyReport:Content"].ToString() + getGitContent(postData);

            return postData;
        }

        private string getGitContent(DailyReportPostData postData)
        {
            var repos = gitTool.GetAllCommits();
            var gitConent = new StringBuilder();
            var isNoData = new List<bool>();

            foreach (var repo in repos)
            {
                var commits = repo.Value.Where(r => r.Committer.When >= postData.StartDate &&
                r.Committer.When < postData.StartDate.AddDays(1));

                if (!commits.Any())
                {
                    isNoData.Add(false);
                }
                else
                {
                    isNoData.Add(true);
                    gitConent.Append($"Project：{repo.Key}\n");
                    var commitIndex = 1;
                    foreach (var commit in commits)
                    {
                        gitConent.Append($"{commitIndex}:{commit.Message}\n");
                        commitIndex++;
                    }
                }
            }

            postData.IsNoData = isNoData.All((n) => !n);

            if (postData.IsNoData)
            {
                MessageBox.Show("There is no dailyData In Today!");
            }


            return gitConent.ToString();
        }
    }
}
