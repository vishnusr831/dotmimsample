using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotmim.Sync.Filter;
using Dotmim.Sync.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApplication2
{
    public class Startup
    {
        private string SqlServerCnxString = @"Data Source=(localdb)\ProjectsV13;Initial Catalog=betatest;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSyncServer<SqlSyncProvider>(SqlServerCnxString, (conf) =>
            {
                var tables = new string[] { "beta" };
                conf.Add(tables);
                var filterClause1 = new FilterClause("beta", "userid");
                conf.Filters.Add(filterClause1);
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
