using GitPowered.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GitPowered.Data
{
    public class PostRepository
    {
        private readonly FileStore _files;
        private readonly string _baseBath;

        public PostRepository(string basePath)
        {
            _baseBath = $@"{basePath}\posts";
            _files = new FileStore(_baseBath);
        }

        public IEnumerable<Post> GetAll()
        {
            var files = _files.GetFiles("*.md");
            var posts = new List<Post>();

            foreach (var file in files)
            {
                var contents = ReadFile(file);
                var post = Post.Parse(contents);

                if (post != null)
                {
                    posts.Add(post);
                }
            }

            return posts.ToArray();
        }

        public Post GetPost(string path)
        {
            var filename = $@"{_baseBath}\{path}.md";
            var content = ReadFile(filename);

            if (content != null)
            {
                return Post.Parse(content);
            }

            return null;
        }

        private string ReadFile(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }

            return null;
        }

    }
}
