using System;
using System.IO;
using DevExpress.AspNetCore;
using DevExpress.AspNetCore.Reporting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DevExpress.XtraReports.Web.Extensions;
using DevExpress.Security.Resources;
using Microsoft.EntityFrameworkCore;
using ReportWizardCustomizationServiceAngularExample.Services;
using ReportWizardCustomizationServiceAngularExample.Data;
using DevExpress.XtraReports.Web.ReportDesigner.Services;

namespace ReportWizardCustomizationServiceAngularExample {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDevExpressControls();
            services.AddScoped<ReportStorageWebExtension, CustomReportStorageWebExtension>();
            services
                .AddMvc()
                .AddNewtonsoftJson();
            services.ConfigureReportingServices(configurator => {
                configurator.ConfigureReportDesigner(designerConfigurator => {
                    designerConfigurator.RegisterDataSourceWizardConnectionStringsProvider<CustomSqlDataSourceWizardConnectionStringsProvider>();
                    designerConfigurator.RegisterDataSourceWizardJsonConnectionStorage<CustomDataSourceWizardJsonDataConnectionStorage>(true);
                    designerConfigurator.RegisterObjectDataSourceWizardTypeProvider<ObjectDataSourceWizardCustomTypeProvider>();
                });
                configurator.ConfigureWebDocumentViewer(viewerConfigurator => {
                    viewerConfigurator.UseCachedReportSourceBuilder();
                    viewerConfigurator.RegisterJsonDataConnectionProviderFactory<CustomJsonDataConnectionProviderFactory>();
                    viewerConfigurator.RegisterConnectionProviderFactory<CustomSqlDataConnectionProviderFactory>();
                });
                configurator.UseAsyncEngine();
            });
            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddDbContext<ReportDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("ReportsDataConnectionString")));
            services.AddScoped<ReportWizardCustomizationService, InstantReportWizardCustomizationService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, ReportDbContext db) {
            db.InitializeDatabase();
            var contentDirectoryAllowRule = DirectoryAccessRule.Allow(new DirectoryInfo(Path.Combine(env.ContentRootPath, "..", "Content")).FullName);
            AccessSettings.ReportingSpecificResources.TrySetRules(contentDirectoryAllowRule, UrlAccessRule.Allow());
            DevExpress.XtraReports.Configuration.Settings.Default.UserDesignerOptions.DataBindingMode = DevExpress.XtraReports.UI.DataBindingMode.Expressions;
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment()) {
                app.UseSpaStaticFiles();
            }
            
            app.UseRouting();
            
            app.UseDevExpressControls();
            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            
            app.UseSpa(spa => {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) {
                    spa.UseAngularCliServer(npmScript: "start");
                    spa.Options.StartupTimeout = TimeSpan.FromSeconds(240);
                }
            });
        }
    }
}
