using GoC.WebTemplate.Components.Core.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using System.Threading.Tasks;

namespace Esdc.Ccoe.AspNetCore.Mvc.Cdts.Aad
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
            // For GoC.WebTemplate.
            services.AddModelAccessor();

            // For GoC.WebTemplate.
            services.ConfigureGoCTemplateRequestLocalization();

            var initialScopes = Configuration["DownstreamApi:Scopes"]?.Split(' ');
            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"))
                // You could put "initalScopes" in the parameter of the next method. 
                // This will ask EVERY permission that your AAD App needs. At first login, it will ask the user to consent for all permissions 
                // even if that specific user don't need all of them.
                // If not, the consent of these permissions will be ask incrementally when calling method that needs them.
                // This is a better way to do it but it depends on your use case and your app, it is up to you.
                .EnableTokenAcquisitionToCallDownstreamApi()
                .AddMicrosoftGraph(Configuration.GetSection("DownstreamApi"))
                // This saves the token of the user in memory. For example, the first time the user logs in after the server is rebooted,
                // if the user reach the private page, MSAL will request a token (with the scopes specified in the method) to get the Users
                // and saves it in memory. Next time the user reach the page, MSAL will make the request directly to Graph API except if the token
                // is expired.
                .AddInMemoryTokenCaches();

            services.Configure<OpenIdConnectOptions>(OpenIdConnectDefaults.AuthenticationScheme, options => {
                options.Events.OnSignedOutCallbackRedirect = context => {
                    // This is to redirect to home when the user sign out. It is the simplest way to
                    // redirect the user when MSAL sign out the user.
                    context.HttpContext.Response.Redirect(context.Options.SignedOutRedirectUri);
                    context.HandleResponse();

                    return Task.CompletedTask;
                };

            });

            services.AddControllersWithViews(options => {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // For GoC.WebTemplate.
            app.UseRequestLocalization();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
