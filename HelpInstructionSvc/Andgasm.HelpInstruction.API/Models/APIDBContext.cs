using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Andgasm.HelpInstruction.API
{
    public class APIDBContext : DbContext
    {
        public APIDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<HelpInstruction> HelpInstructions { get; set; }
    }
}
