using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TeamTwo.CloudShield.DemoServerStartup
{
  public static class Program
  {
#pragma warning disable IDE1006 // Naming Styles
    public static async Task Main(string[] args)
#pragma warning restore IDE1006 // Naming Styles
    {
      IWebHost builder = CreateWebHostBuilder(args);
      var thread = new Thread(() => builder.Run());
      thread.Start();
    }

    public static IWebHost CreateWebHostBuilder(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
.UseKestrel()
.UseStartup<TeamTwo.CloudShield.DemoApiServer.Startup>()
.ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Warning))
.UseUrls("http://0.0.0.0:5000/")
.Build();
    }
  }
}
