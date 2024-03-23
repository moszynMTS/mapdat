// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using AuthorizationServer.Data;
using AuthorizationServer.Extensions;
using AuthorizationServer.Models;
using AuthorizationServer.PolicyCode;
using AuthorizationServer.Services;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using System.Linq;
using System.Reflection;

namespace AuthorizationServer
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(environment.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCustomExceptionHandler();
            var seed = Configuration["Setup:CreateAndSeed"];
            if (seed == "True")
            {
                InitializeDatabase(app);
            }
            //ClearDatabase(app);
            //app.UseDeveloperExceptionPage();
            if (Environment.IsDevelopment())
            {
                app.UseDatabaseErrorPage();
                IdentityModelEventSource.ShowPII = true;
            }

            app.UseStaticFiles();

            #region nginx https redirections

            var forwardOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                // Needed because of mixing http and https.
                RequireHeaderSymmetry = false,
            };

            // Accept X-Forwarded-* headers from all sources.
            forwardOptions.KnownNetworks.Clear();
            forwardOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(forwardOptions);

            #endregion nginx https redirections

            var useSwagger = Configuration["Setup:UseSwagger"];
            if (useSwagger == "True")
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseCors("cors");
            app.UseIdentityServer();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddControllersWithViews();

            // configures IIS out-of-proc settings (see https://github.com/aspnet/AspNetCore/issues/14882)
            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            // configures IIS in-proc settings
            services.Configure<IISServerOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                sql => sql.MigrationsAssembly(migrationsAssembly))
            );

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 3600 * 24; // perform cleanup of expired tokens once per day
                })
                .AddAspNetIdentity<ApplicationUser>();

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            builder.Services.ConfigureExternalCookie(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Unspecified;
            });
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.Unspecified;
            });

            ////cors policy for js UI
            services.AddCors(setup =>
            {
                setup.AddPolicy("cors", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                    //policy.WithOrigins("http://localhost:4200");
                    //policy.AllowCredentials();
                });
            });

            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(c => Configuration.GetSection("EmailServer").Get<EmailConfiguration>());
            services.AddSingleton(c => Configuration.GetSection("CreateAccountOptions").Get<CreateAccountOptions>());
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<IClientsService, ClientsService>();
            services.AddTransient<IResourcesService, ResourcesService>();
            services.AddTransient<IProfileService, CustomProfileService>();

            services.AddLocalApiAuthentication();

            services.AddSwaggerDocument();

            services.AddAuthentication();
        }

        private void EnsureResourceSeed(ConfigurationDbContext context)
        {
            var clients = context.Clients.ToList();
            foreach (var client in Config.Clients)
            {
                if (!clients.Any(_ => _.ClientId == client.ClientId))
                {
                    context.Clients.Add(client.ToEntity());
                }
            }

            var identityResources = context.IdentityResources.ToList();
            foreach (var ir in Config.Ids)
            {
                if (!identityResources.Any(_ => _.Name == ir.Name))
                {
                    context.IdentityResources.Add(ir.ToEntity());
                }
            }

            var apiResources = context.ApiResources.ToList();
            foreach (var ar in Config.Apis)
            {
                if (!apiResources.Any(_ => _.Name == ar.Name))
                {
                    context.ApiResources.Add(ar.ToEntity());
                }
            }
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                context.Database.Migrate();
                EnsureResourceSeed(context);

                context.SaveChanges();

                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                SeedData.EnsureSeedData(connectionString);
            }
        }

        private void ClearDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                context.Clients.RemoveRange(context.Clients);
                context.IdentityResources.RemoveRange(context.IdentityResources);
                context.ApiResources.RemoveRange(context.ApiResources);
                context.SaveChanges();
            }
        }
    }
}