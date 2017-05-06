using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GtfsApi.Data;
using GtfsApi.Models;
using GtfsApi.Services;
using Swashbuckle.AspNetCore.Swagger;
using GTFSManagerApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GtfsApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<MyIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

			services.AddDbContext<GtfsContext>(options => 
				options.UseSqlServer(Configuration.GetConnectionString("GtfsConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MyIdentityDbContext>()
                .AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(config =>
			{
				config.Cookies.ApplicationCookie.Events =
					new CookieAuthenticationEvents
					{
						OnRedirectToLogin = ctx =>
						{
							if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
							{
								ctx.Response.StatusCode = 401;
								return Task.FromResult<object>(null);
							}

							ctx.Response.Redirect(ctx.RedirectUri);
							return Task.FromResult<object>(null);
						}
					};
			});

			services.AddMvc();

			services.AddLogging();

			// Register the Swagger generator, defining one or more Swagger documents
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
			});

			// Add application services.
			services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
			
			// Serve my app-specific default file, if present.
			//DefaultFilesOptions options = new DefaultFilesOptions();
			//options.DefaultFileNames.Clear();
			//options.DefaultFileNames.Add("index.html");
			//app.UseDefaultFiles(options);
			app.UseDefaultFiles();

			app.UseStaticFiles();

            app.UseIdentity(); // https://github.com/tjoudeh/AspNetIdentity.WebApi http://bitoftech.net/2015/01/21/asp-net-identity-2-with-asp-net-web-api-2-accounts-management/

			// Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

			// Enable middleware to serve generated Swagger as a JSON endpoint.
			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});
		}
    }
}
