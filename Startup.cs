using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using AMRS.Models;
using Microsoft.EntityFrameworkCore.Internal;
using AMRS.Controllers;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509.Qualified;
using Microsoft.Extensions.Options;

namespace ARMS
{
    public class Startup
    {

       public IConfiguration Configuration { get; }
        public Startup(Microsoft.Extensions.Hosting.IHostingEnvironment env, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();            
            //ConnectionString = Configuration.GetSection("ConnectionStrings")["Defaultconnection"];
             Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();                 
            services.Add(new ServiceDescriptor(typeof(AMRSDBContext), new AMRSDBContext(Configuration.GetConnectionString("DefaultConnection")))); 

                       
            services.AddMvc();
                        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Hosting.IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddProvider((ILoggerProvider)Configuration.GetSection("Logging"));
                                                     
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
                app.UseStaticFiles();            
        }
    }
}