using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using GitPowered.Models.Github;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GitPowered.Controllers
{
    [Route("api/[controller]")]
    public class PushWebhookController : Controller
    {
        // POST api/values
        [HttpPost]
        public void Post([FromBody]PushPayload payload)
        {
        }
    }
}
