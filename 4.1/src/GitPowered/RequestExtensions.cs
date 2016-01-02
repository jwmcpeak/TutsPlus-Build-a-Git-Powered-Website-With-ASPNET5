using GitPowered.Models.Github;
using GitPowered.Validation;
using Microsoft.AspNet.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitPowered
{
    public static class RequestExtensions
    {
        public static async Task<PushPayload> GetGithubPayloadAsync(this HttpRequest request, string secretKey)
        {
            var body = await request.Body.ReadToEndAsync();
            var payload = JsonConvert.DeserializeObject<PushPayload>(body);
            var header = request.Headers["X-Hub-Signature"];

            if (!GithubValidate.Validate(header, secretKey, body))
            {
                return null;
            }

            return payload;
        }
    }
}
