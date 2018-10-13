using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Andgasm.HelpInstruction.Web.Component
{
    public class HelpInstructionModel
    {
        public string label { get; set; }
        public string lookupKey { get; set; }
        public string tooltipText { get; set; }
        public string imageurl { get; set; }
        public bool suppressScripts { get; set; }
        public bool suppressStyles { get; set; }
        public bool loadOnDemand { get; set; }
        public string apiUrl { get; set; }
    }
}
