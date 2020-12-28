using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SharingTransactionApp.Models;
using SharingTransactionApp.Models.Services;
using SharingTransactionApp.Models.Inerfaces;
using System;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace SharingTransactionApp
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

            services.AddControllersWithViews();
            
            services.Configure<SettingsDB>(options =>
            {
                options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.DatabaseName = Configuration.GetSection("MongoConnection:DatabaseName").Value;
                options.StorageCollectionName = Configuration.GetSection("MongoConnection:StorageCollectionName").Value;
            });
            services.AddSingleton<ISettingsDB>(sp =>
                sp.GetRequiredService<IOptions<SettingsDB>>().Value);
            services.AddSingleton<IMongoService>(sp=>new MongoService(sp.GetRequiredService<ISettingsDB>()));
            services.AddScoped<ITransactionRegistrar, TransactionRegistrar>();
            services.AddScoped<IBalanceService, BalanceService>();
            services.AddScoped<IBalanceUpdater, BalanceUpdater>();
            services.AddRazorPages();

           
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddTransient<CookieEventHandler>();
            services.AddSingleton<LogoutSessionManager>();
            services.AddCors(options =>
            {
                options.AddPolicy(name: "test",
                                  builder =>
                                  {
                                      builder.WithOrigins("https://localhost:5001")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod(); ;
                                  });
            });
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "test123";

                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("test123", options => {
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromSeconds(10);

                    options.EventsType = typeof(CookieEventHandler);
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.ClientId = "sharingClient";
                    options.ClientSecret = "SuperSecretPassword";
                    options.RequireHttpsMetadata = true;
                    options.ResponseType = "code";
                    options.UsePkce = true;
                    options.ResponseMode = "query";

                    options.CallbackPath = "/signin-oidc"; 

                    options.Events.OnTicketReceived = async (context) =>
                    {
                        context.Properties.ExpiresUtc = DateTime.UtcNow.AddSeconds(1000);
                    };
                    options.UseTokenLifetime = true;
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.Scope.Add("api1.read");
                    options.Scope.Add("role");
                    options.Scope.Add("email");
                    options.Events = new OpenIdConnectEvents()
                    {
                        OnTokenValidated = context =>
                        {
                            return Task.FromResult(0);
                        },
                        OnUserInformationReceived = context =>
                        {
                            return Task.FromResult(0);
                        }
                    };
                    options.SaveTokens = true;
                });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
              
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            
            var fileExtensionProvider = new FileExtensionContentTypeProvider();
            fileExtensionProvider.Mappings[".webmanifest"] = "application/manifest+json";
            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = fileExtensionProvider,
                RequestPath = "",
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp/build"))
            });
            app.UseCors("test");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Response.StatusCode == 404)
                {
                    context.HttpContext.Response.Redirect("/");
                }
            });

        }
    }
}
