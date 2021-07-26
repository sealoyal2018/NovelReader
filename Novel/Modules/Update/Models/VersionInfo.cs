using System;
using System.Collections.Generic;

namespace Novel.Modules.Update.Models {
    public class VersionInfo {
        public string LatestVersion { get; set; }
        public List<string> Summary { get; set; } = new List<string>();
        public DateTime UpdateTime { get; set; }
        public string Token { get; set; }
        public List<VersionInfo> OldVersionInfo { get; set; } = new List<VersionInfo>();
    }
}
