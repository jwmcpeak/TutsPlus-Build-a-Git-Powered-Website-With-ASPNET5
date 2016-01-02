using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using GitPowered.Models.Github;
using Newtonsoft.Json;
using GitPowered.Validation;
using Microsoft.AspNet.Hosting;
using GitPowered.Services;
using GitPowered.Config;
using Microsoft.Extensions.OptionsModel;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GitPowered.Controllers
{
    [Route("api/[controller]")]
    public class PushWebhookController : Controller
    {
        private readonly IHostingEnvironment _environment;
        private readonly GithubPushConfig _config;
        public PushWebhookController(IHostingEnvironment environment, IOptions<GithubPushConfig> options)
        {
            _environment = environment;
            _config = options.Value;
        }

        // POST api/values
        [HttpPost]
        public async Task Post(/*[FromBody]PushPayload payload*/)
        {
            var payload = await Request.GetGithubPayloadAsync(_config.SecretKey);

            if (payload == null)
            {
                Response.StatusCode = 400;
                return;
            }

            var commits = payload.Commits.OrderByDescending(c => c.Timestamp);
            var processor = new GithubPushProcessor(_environment.WebRootPath, _config);

            foreach (var commit in commits)
            {
                await processor.ProcessAsync(commit);
            }
 
            // todo: process

            int foo = 0;
        }
    }
}
