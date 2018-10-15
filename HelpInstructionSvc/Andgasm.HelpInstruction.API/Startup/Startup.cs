using Andgasm.API.Core;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NJsonSchema;
using NSwag.AspNetCore;
using System;
using System.Reflection;

namespace Andgasm.HelpInstruction.API.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddDbContext<APIDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();
            services.AddSwagger();
            services.AddSingleton<DynamicExpressionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUi3WithApiExplorer(settings =>
                {
                    settings.GeneratorSettings.Title = "Help Instruction Service";
                    settings.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;
                    settings.GeneratorSettings.DefaultEnumHandling = EnumHandling.String;
                });
                InitialiseData(app.ApplicationServices);
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public static async void InitialiseData(IServiceProvider svcs)
        {
            // but of a hack here but this just ensure that the db exists and that the basic data is configured
            // helps support basic ui tests as well as expected config for the admin web help instructions

            using (var servicescope = svcs.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = servicescope.ServiceProvider.GetService<APIDBContext>();
                await context.Database.EnsureCreatedAsync();

                if (!await context.HelpInstructions.AnyAsync(x => x.HostKey == "HelpInstructionSvc" && x.LookupKey == "Host"))
                {
                    var hi1 = new HelpInstruction() { HostKey = "HelpInstructionSvc", LookupKey = "Host", TooltipText = "I am the host tooltip text!" };
                    await context.HelpInstructions.AddAsync(hi1);
                }
                if (!await context.HelpInstructions.AnyAsync(x => x.HostKey == "HelpInstructionSvc" && x.LookupKey == "LookupKey"))
                {
                    var hi2 = new HelpInstruction() { HostKey = "HelpInstructionSvc", LookupKey = "LookupKey", TooltipText = "I am the lookup key tooltip text!" };
                    await context.HelpInstructions.AddAsync(hi2);
                }
                if (!await context.HelpInstructions.AnyAsync(x => x.HostKey == "HelpInstructionSvc" && x.LookupKey == "TooltipText"))
                {
                    var hi3 = new HelpInstruction() { HostKey = "HelpInstructionSvc", LookupKey = "TooltipText", TooltipText = "I am the tooltip text tooltip text!" };
                    await context.HelpInstructions.AddAsync(hi3);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
