using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UploadImageAPI.Services.Implementations;
using UploadImageAPI.Services.Interfaces;
using UploadImageAPI.Writers.ImageWriter;

namespace UploadImageAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<IImageWriter, ImageWriter>();
            services.AddTransient<IUploadImageService, UploadImageService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            // graceful shutdown
            appLifetime.ApplicationStopping.Register(() => { Console.WriteLine("Upload Image API is shut down"); });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}