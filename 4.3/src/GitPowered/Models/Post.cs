using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarkdownSharp;

namespace GitPowered.Models
{
    public class Post
    {
        public string Title { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string Content { get; private set; }

        public static Post Parse(string fileData)
        {
            var lines = fileData.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var metadata = new Dictionary<string, string>();

            if (lines.Any() && lines[0] != "---")
            {
                return null;
            }

            var ii = 1;
            for (; ii < lines.Length; ii++ )
            {
                if (lines[ii] == "---")
                {
                    break;
                }

                var parts = lines[ii].Split(':');
                metadata.Add(parts[0].Trim(), parts[1].Trim());
            }

            var content = string.Join(Environment.NewLine, lines.Skip(ii + 1).ToArray());

            var md = new Markdown();

            return new Post
            {
                Title = metadata["title"],
                PublishDate = DateTime.Parse(metadata["date"]),
                Content = md.Transform(content)
            };
        }
    }
}
