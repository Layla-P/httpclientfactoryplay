using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using HttpClientFactory.Models;
using HttpClientFactory.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HttpClientFactory
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<TwilioAccount>(Configuration.GetSection("TwilioAccount"));

            //register the named HttpFactory Service which will add in various services includes an implementation of IHttpClientFactory
            services.AddHttpClient<TwilioHttpClient>(client =>
            {
                client
                    .BaseAddress = new Uri("https://api.twilio.com/2010-04-01/");

                var account = Configuration.GetSection("TwilioAccount:AccountSid").Value;
                var secret = Configuration.GetSection("TwilioAccount:AuthToken").Value;
                var httpAuthentication = Encoding.ASCII.GetBytes($"{account}:{secret}");

                client
                    .DefaultRequestHeaders
                    .Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(httpAuthentication));
            });

            

            services.AddTransient<INumberDetailsService, NumberDetailsService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        
    }
}


