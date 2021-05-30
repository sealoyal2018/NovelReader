using Novel.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel {
    public interface IDocument {
        public string Name { get; }
        public string Icon { get; }
        public bool Show { get; }
        public int Index { get; }
    }
}
