using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GitPowered.Validation
{
    public class GithubValidate
    {
        public static bool Validate(string signature, string requestBody)
        {
            var secret = "ducks fly with geese";

            if (string.IsNullOrWhiteSpace(signature))
            {
                return false;
            }

            var hash = ComputeBodyHash(secret, requestBody);

            return hash == signature;
        }

        private static string ComputeBodyHash(string secret, string body)
        {
            var secretByteArray = Encoding.ASCII.GetBytes(secret);
            var bodyByteArray = Encoding.ASCII.GetBytes(body);

            using (var hmac = new HMACSHA1(secretByteArray))
            {
                var hash = hmac.ComputeHash(bodyByteArray);
                var hex = BitConverter.ToString(hash);

                return hex.Replace("-", string.Empty).ToLower();
            }

        }
    }
}
