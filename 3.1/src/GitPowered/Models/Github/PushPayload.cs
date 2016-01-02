using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitPowered.Models.Github
{
    public class PushPayload
    {
        public IList<Commit> Commits{ get; set; }
    }

    public class Commit
    {
        public DateTime Timestamp { get; set; }
        public IList<string> Added { get; set; }
        public IList<string> Modified { get; set; }
        public IList<string> Removed { get; set; }
    }
}
