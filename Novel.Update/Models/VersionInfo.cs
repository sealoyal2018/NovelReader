using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Update.Models {
    public class VersionInfo {
        public string LatestVersion { get; set; }
        public List<string> Summary { get; set; } = new List<string>();
        public DateTime UpdateTime { get; set; }
        public Guid Token { get; set; }
        public List<VersionInfo> OldVersionInfo { get; set; } = new List<VersionInfo>();
    }
}
