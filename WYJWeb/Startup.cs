using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WYJCore;
using WYJCore.Extension;
using WYJWeb.Models;

namespace WYJWeb
{
    public class Startup
    {
        public static IConfiguration Configuration;
        public int alert;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddWYJRouter(); 
            services.AddHangfire(configuration => configuration
            .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            app.UseWYJRouter(Run);

        }

        public void Run()
        {
            RecurringJob.AddOrUpdate("clock", () => RunColock(), "0 0 * * ?", TimeZoneInfo.Local);
        }

        public void RunColock()
        {
            var db = new DbContext<Clock>("WYJ");
            var today = DateTime.Today;

            var clocklist = db.GetList();
            clocklist.ForEach(x =>
            {
                if(Clock.AlertAtNow(x)) Clock.Alert();
            });

        }
    }
}
