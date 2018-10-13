using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SE.DynamicHelp.API.Resources
{
    public class HelpInstructionResource
    {
        public int InternalId { get; set; }
        public string ExternalId { get; set; }

        [Required]
        public string HostKey { get; set; }
        [Required]
        public string LookupKey { get; set; }
        [Required]
        public string TooltipText { get; set; }
    }
}
