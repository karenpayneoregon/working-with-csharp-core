using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseOracleCoreConsole1.Classes
{
    public class Settings
    {
        public string Title { get; set; }
        public bool FullScreen { get; set; }
        public override string ToString() => Title;
    }
}
