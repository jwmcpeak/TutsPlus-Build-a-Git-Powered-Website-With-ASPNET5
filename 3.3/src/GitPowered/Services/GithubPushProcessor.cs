using GitPowered.Data;
using GitPowered.Models.Github;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitPowered.Services
{
    public class GithubPushProcessor
    {
        private readonly List<string> _processed = new List<string>();
        private readonly FileStore _files;

        public GithubPushProcessor(string wwwrootPath)
        {
            _files = new FileStore(wwwrootPath);
        }
        public async Task ProcessAsync(Commit commit)
        {
            var filesToRetrieve = commit.Added.Union(commit.Modified);
            var baseUrl = "https://raw.githubusercontent.com/jwmcpeak/hookrepo/master";

            foreach (var file in filesToRetrieve)
            {
                if (_processed.Contains(file))
                {
                    continue;
                }

                var url = $@"{baseUrl}/{file}";
                var data = await GetFileAsync(url);
                await _files.CreateOrUpdateAsync(file, data);
                _processed.Add(file);
            }

            foreach (var file in commit.Removed)
            {
                await _files.DeleteAsync(file);
            }
        }

        private async Task<string> GetFileAsync(string url)
        {
            using (var client = new HttpClient())
            {
                return await client.GetStringAsync(url);
            }
        }
    }
}
