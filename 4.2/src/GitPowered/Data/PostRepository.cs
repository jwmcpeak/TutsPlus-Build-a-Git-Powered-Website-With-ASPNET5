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

        public PostRepository(string basePath)
        {
            _files = new FileStore($@"{basePath}\posts");
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
            var content = ReadFile(path + ".md");

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
