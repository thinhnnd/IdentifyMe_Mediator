using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentifyMe_Mediator
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAriesFramework(builder =>
            {
                builder.RegisterMediatorAgent(options =>
                {
                    #region Required configuration parameters
                    // Agent endpoint. Use fully qualified endpoint.
                    
                    options.EndpointUri = "https://im-mediator.herokuapp.com";
                    #endregion

                    #region Optional configuration parameters
                    // The identifier of the wallet
                    options.WalletConfiguration.Id = "MyAgentWallet";
                    // Secret key used to open the wallet.
                    options.WalletCredentials.Key = "MySecretKey";
                    #endregion
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAriesFramework();
            app.UseMediatorDiscovery();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Mediator Worked");
                });
            });
        }
    }
}
