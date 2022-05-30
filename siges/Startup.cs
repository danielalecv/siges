//noreply. juqsak-numzyr-6zEgdi sendgrid erick.agutierrez@roatech.com.mx
// "suwusysiges" was successfully created and added to the next step.
// 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
// Create an environment variable
// Update the development environment (user space) with your SENDGRID_API_KEY. For example, in Windows 10, please review this thread.
// // using SendGrid's C# Library
// // https://github.com/sendgrid/sendgrid-csharp
// using SendGrid;
// using SendGrid.Helpers.Mail;
// using System;
// using System.Threading.Tasks;
//
// namespace Example
// {
//  internal class Example
//    {
//      private static void Main()
//       {
//       Execute().Wait();
//       }

//     static async Task Execute()
//    {
//      var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
//      var client = new SendGridClient(apiKey);
//      var from = new EmailAddress("test@example.com", "Example User");
//      var subject = "Sending with SendGrid is Fun";
//      var to = new EmailAddress("test@example.com", "Example User");
//      var plainTextContent = "and easy to do anywhere, even with C#";
//      var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
//      var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
//      var response = await client.SendEmailAsync(msg);
//      }
//      }
//      }
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Globalization;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using siges.Services;
using siges.Data;
using siges.Repository;
using siges.Areas.Identity.Data;

namespace siges {
  public class Startup {

    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      services.Configure<CookiePolicyOptions>(options => {
          options.CheckConsentNeeded = context => true;
          options.MinimumSameSitePolicy = SameSiteMode.None;
          });
      services.AddAuthentication(AzureADDefaults.AuthenticationScheme).AddAzureAD(options => Configuration.Bind("AzureAd", options));
      services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options => {
          options.Authority = options.Authority + "/v2.0/";
          options.TokenValidationParameters.ValidateIssuer = false;
          });
      services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer( Configuration.GetConnectionString("DefaultConnection")));
      services.AddDefaultIdentity<RoatechIdentityUser>(options => options.SignIn.RequireConfirmedEmail = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
      //Change email and activity timeout
      services.ConfigureApplicationCookie(o => {
          //o.ExpireTimeSpan = TimeSpan.FromMinutes(20);
          o.ExpireTimeSpan = TimeSpan.FromMinutes(4*60);
          o.SlidingExpiration = true; });
      services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromMinutes(20));

      services.AddMvc(
          config => {
          var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
          config.Filters.Add(new AuthorizeFilter(policy));
          config.EnableEndpointRouting = false;
          }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

      services.Configure<AuthMessageSenderOptions>(Configuration);
      //services.AddRazorPages();
      services.Configure<IdentityOptions>(options => {
          // Password settings.
          options.Password.RequireDigit = true;
          options.Password.RequireLowercase = true;
          options.Password.RequireNonAlphanumeric = true;
          options.Password.RequireUppercase = true;
          options.Password.RequiredLength = 8;
          options.Password.RequiredUniqueChars = 1;
          // Lockout settings.
          options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
          options.Lockout.MaxFailedAccessAttempts = 5;
          options.Lockout.AllowedForNewUsers = true;
          // User settings.
          options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
          options.User.RequireUniqueEmail = true;
          });
      services.ConfigureApplicationCookie(options =>
          {
          // Cookie settings
          options.Cookie.HttpOnly = true;
          options.ExpireTimeSpan = TimeSpan.FromMinutes(4*60);
          //options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
          options.LoginPath = "/Identity/Account/Login";
          options.LogoutPath ="/Identity/Account/Login";
          options.AccessDeniedPath = "/Identity/Account/Login";
          //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
          options.SlidingExpiration = true;
          });

      services.AddScoped<IPersonaRepository, PersonaRepository>();
      services.AddScoped<IRoatechIdentityUserRepo, RoatechIdentityUserRepo>();

      services.AddScoped<IActivoFijoRepository, ActivoFijoRepository>();
      services.AddScoped<IContratoRepository, ContratoRepository>();
      services.AddScoped<IInsumoRepository, InsumoRepository>();
      services.AddScoped<ILineaNegocioRepository, LineaNegocioRepository>();
      services.AddScoped<IPersonaRepository, PersonaRepository>();
      services.AddScoped<IServicioRepository, ServicioRepository>();
      services.AddScoped<IUbicacionRepository, UbicacionRepository>();
      services.AddScoped<IClienteRepository, ClienteRepository>();
      services.AddScoped<IDetalleActivoFijoRepository, DetalleActivoFijoRepository>();
      services.AddScoped<IEntradaActivoFijoRepository, EntradaActivoFijoRepository>();
      services.AddScoped<IConciliacionActivoFijoRepository, ConciliacionActivoFijoRepository>();
      services.AddScoped<IConciliacionInsumoRepository, ConciliacionInsumoRepository>();
      services.AddScoped<IConfiguracionServicioRepository, ConfiguracionServicioRepository>();
      services.AddScoped<IDetalleConfiguracionServicioRepository, DetalleConfiguracionServicioRepository>();
      services.AddScoped<IDetalleInsumoRepository, DetalleInsumoRepository>();
      services.AddScoped<IEntradaInsumoRepository, EntradaInsumoRepository>();
      services.AddScoped<IOrdenServicioRepository, OrdenServicioRepository>();
      services.AddScoped<IInventarioActivoFijoRepository, InventarioActivoFijoRepository>();
      services.AddScoped<IInventarioInsumoRepository, InventarioInsumoRepository>();
      services.AddScoped<ITraspasoActivoFijoRepository, TraspasoActivoFijoRepository>();
      services.AddScoped<ITraspasoInsumoRepository, TraspasoInsumoRepository>();
      services.AddScoped<IBitacoraRepository, BitacoraRepository>();
      services.AddScoped<IOrdenPersona, OrdenPersonaRepository>();
      services.AddScoped<IOperador, OperadorRepository>();
      services.AddScoped<IInventarioAF, InventarioAFRepository>();
      services.AddScoped<IAdministracion, AdministracionRepository>();
      services.AddScoped<IComercial, ComercialRepository>();
      services.AddScoped<IBitacoraEstatusRepository, BitacoraEstatusRepository>();
      services.AddScoped<ISettingsRepository, SettingsRepository>();
      services.AddScoped<IOrdenActivoFijo, OrdenActivoFijoRepository>();
      services.AddScoped<IProducto, ProductoRepository>();
      services.AddScoped<ISemaphoreParamsRepo, SemaphoreParamsRepo>();
      services.AddScoped<IOrdenInsumo, OrdenInsumoRepository>();
      services.AddScoped<ILote, LoteRepository>();
      services.AddScoped<IPaquete, PaqueteRepository>();
      services.AddScoped<IKit, KitRepository>();
      services.AddScoped<IPaqueteInsumo, PaqueteInsumoRepository>();
      services.AddScoped<IKitInsumo, KitInsumoRepository>();
      services.AddScoped<IPaqueteInsumo, PaqueteInsumoRepository>();
      services.AddScoped<IInventarioI, InventarioIRepository>();
      services.AddScoped<IClienteIdentity, ClienteIdentityRepository>();
      services.AddScoped<IArchivo, ArchivoRepository>();
      services.AddScoped<IMarca, MarcaRepository>();
      services.AddScoped<ITipoProducto, TipoProductoRepository>();
      services.AddScoped<IContactoCliente, ContactoClienteRepository>();
      services.AddScoped<IBulkUploadTemplate, BulkUploadTemplateRepository>();
      services.AddScoped<IIndexListOSDTORepository, IndexListOSDTORepository>();
      services.AddScoped<IMailSupport, MailSupportRepository>();
      services.AddScoped<IClientContactService, ClientContactService>();

      services.AddSingleton<siges.Utilities.IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<siges.Utilities.EmailConfiguration>());
      services.AddTransient<siges.Services.IEmailSender, siges.Services.EmailSender>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    // IHostingEnvironment Interface
    // Definition
    // Namespace:
    // Microsoft.AspNetCore.Hosting
    // Assembly:
    // Microsoft.AspNetCore.Hosting.Abstractions.dll
    // Warning
    // This API is now obsolete.
    // Provides information about the web hosting environment an application is running in.
    // This type is obsolete and will be removed in a future version. The recommended alternative is Microsoft.AspNetCore.Hosting.IWebHostEnvironment.
    // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.ihostingenvironment?view=aspnetcore-2.2
    public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
        //app.UseDatabaseErrorPage();
      } else {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      var cultureInfo = new CultureInfo("es-ES");
      //cultureInfo.DateTimeFormat.LongDatePattern = "dd/MM/yyyy";
      cultureInfo.NumberFormat.CurrencySymbol = "$";
      cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
      cultureInfo.NumberFormat.CurrencyDecimalSeparator = ".";
      cultureInfo.NumberFormat.CurrencyGroupSeparator = ",";

      CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
      CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-2.2
      //app.UseRouting();

      app.UseCookiePolicy();
      app.UseAuthentication();
      //app.UseAuthorization();

      //app.UseEndpoints(endpoints => {
          //endpoints.MapControllerRoute(
              //name: "default",
              //pattern: "{controller=Home}/{action=Index}/{id?}");
          //endpoints.MapRazorPages();
          //});
      app.UseMvc(routes => {
          routes.MapRoute(
              name: "default",
              template: "{controller=Home}/{action=Index}/{id?}");
          });
    }
  }
}
