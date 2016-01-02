using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GitPowered
{
    public static class StreamExtensions
    {
        public static async Task<string> ReadToEndAsync(this Stream stream)
        {
            return await new StreamReader(stream).ReadToEndAsync();
        }
    }
}
