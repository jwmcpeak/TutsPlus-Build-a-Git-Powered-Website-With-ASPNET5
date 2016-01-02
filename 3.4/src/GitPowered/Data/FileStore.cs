using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GitPowered.Data
{
    public class FileStore
    {
        private readonly string _basePath;
        public FileStore(string basePath)
        {
            _basePath = basePath;
        }

        public async Task CreateOrUpdateAsync(string filename, string data)
        {
            var path = $@"{_basePath}\{filename}";
            var realFileName = Path.GetFileName(path);
            var folderPath = path.Replace(realFileName, string.Empty);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (var writer = File.CreateText(path))
            {
                await writer.WriteAsync(data);
            }
        }

        public async Task DeleteAsync(string filename)
        {
            var path = $@"{_basePath}\{filename}";
            
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
