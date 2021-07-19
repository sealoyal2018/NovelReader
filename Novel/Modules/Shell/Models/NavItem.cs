using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Modules.Shell.Models
{
    public class NavItem : PropertyChangedBase
    {
        private string name;
        private int order;
        private IDocument document;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public int Order
        {
            get
            {
                return order;
            }

            set
            {
                order = value;
            }
        }

        public IDocument Document
        {
            get
            {
                return document;
            }

            set
            {
                document = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
