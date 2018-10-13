using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Andgasm.HelpInstruction.API
{
    public class PersistentBase
    {
        [Key]
        public int Id { get; set; }
        public string ExternalId { get; set; }
    }
}
