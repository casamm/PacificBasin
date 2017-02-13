using Entitlement.Common.Model.Vo;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Entitlement.Shell.View.Interfaces;

namespace Entitlement.Shell.View.Components
{
    public class Startup
    {
        public Startup()
        {
            ApplicationFacade.GetInstance("Entitlement.Shell").Startup(this);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                if(context.Request.Path.Value == "/")
                {
                    await context.Response.WriteAsync("Hello World!");
                } else
                {
                    await Delegate.Service(new ContextVo(context));
                }
            });
        }

        public IService Delegate { get; set; }
    }
}
