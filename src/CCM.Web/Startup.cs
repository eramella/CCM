using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CCM.Data.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;
using CCM.ViewModels;
using CCM.Services;
using Microsoft.EntityFrameworkCore;
using CCM.Data.Contracts;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.HttpOverrides;

namespace WebApplicationBasic
{
    public class Startup
    {
        private IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var physicalProvider = _env.WebRootFileProvider;
            services.AddSingleton(Configuration);
            services.AddTransient<IUserResolverService, UserResolverService>();
            services.AddDbContext<CCMContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:CCMContextConnection"]);
            });
            services.AddSingleton<IFileProvider>(physicalProvider);
            services.AddTransient<CCMSeedData>();


            services.Configure<CcmSettings>(Configuration.GetSection("CcmSettings"));

            // Add framework services.
            services.AddIdentity<CCMUser, IdentityRole>()
            .AddEntityFrameworkStores<CCMContext>()
            .AddDefaultTokenProviders();

            services.AddMvc(config =>
            {
                if (_env.IsProduction())
                {
                    //config.Filters.Add(new RequireHttpsAttribute());
                }
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));

            })
            .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            //Explanation for Coockie authentication/configuration  https://github.com/aspnet/Announcements/issues/262
            services.ConfigureApplicationCookie(conf =>
            {
                conf.LoginPath = "/Auth/Login";
                conf.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        await Task.Yield();
                    }
                };
            });

            services.Configure<IdentityOptions>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
            });

            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CCMSeedData seeder, IHttpContextAccessor httpContext)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Error);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.Map("/spadist", spaApp =>
                {
                    app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                    {
                        HotModuleReplacement = true
                    });
                });

                app.Map("/pagedist", spaApp =>
                {
                    app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                    {
                        HotModuleReplacement = true,
                        ConfigFile = "webpack.config.page.js"
                    });
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                //Forwarding headers for Nginx reverse proxy
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }

            app.UseStaticFiles();

            app.UseCookiePolicy();
            app.Use((context, next) =>
            {
                context.Features.Set<ITrackingConsentFeature>(new TrackingConsentFeature(context));

                return next();
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Camp", action = "Index" });
            });

            seeder.EnsureSeedData().Wait();
        }

    }
}
