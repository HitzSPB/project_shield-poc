using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TeamTwo.CloudShield.DemoServerStartup
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void Configure(IApplicationBuilder app)
    {
      app.Run(context =>
      {
        var data = System.Text.Encoding.UTF8.GetBytes("Hello World from the ASP.Net CORE!");
        context.Response.Body.Write(data, 0, data.Length);

        return Task.CompletedTask;
      });
    }
  }
}