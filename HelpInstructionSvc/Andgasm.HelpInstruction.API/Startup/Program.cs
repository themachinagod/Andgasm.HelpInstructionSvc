using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Andgasm.HelpInstruction.API.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var b = WebHost.CreateDefaultBuilder(args);
            if (args.Length > 0)
            {
                b.UseUrls(args[0]);
            }
            return b.UseStartup<Startup>();
        }
    }
}
