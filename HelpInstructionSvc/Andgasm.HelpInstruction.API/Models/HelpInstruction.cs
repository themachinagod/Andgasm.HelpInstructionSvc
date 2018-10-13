using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Andgasm.HelpInstruction.API
{
    public class HelpInstruction : PersistentBase
    {
        public string HostKey { get; set; }
        public string LookupKey { get; set; }
        public string TooltipText { get; set; }
    }
}
