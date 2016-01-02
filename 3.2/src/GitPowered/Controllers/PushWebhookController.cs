﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using GitPowered.Models.Github;
using Newtonsoft.Json;
using GitPowered.Validation;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GitPowered.Controllers
{
    [Route("api/[controller]")]
    public class PushWebhookController : Controller
    {
        // POST api/values
        [HttpPost]
        public async Task Post(/*[FromBody]PushPayload payload*/)
        {
            var body = await Request.Body.ReadToEndAsync();
            var payload = JsonConvert.DeserializeObject<PushPayload>(body);
            var header = Request.Headers["X-Hub-Signature"];
            
            if (!GithubValidate.Validate(header, body))
            {
                Response.StatusCode = 400;
                return;
            }

            // todo: process

            int foo = 0;
        }
    }
}
