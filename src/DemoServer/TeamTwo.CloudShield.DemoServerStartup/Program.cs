using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TeamTwo.CloudShield.DemoServerStartup
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      IWebHost builder = CreateWebHostBuilder(args);
      var thread = new Thread(() => builder.Run());
      thread.Start();
    }

    public static IWebHost CreateWebHostBuilder(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
.UseKestrel()
.UseStartup<DemoApiServer.Startup>()
.ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Warning))
.UseUrls("http://0.0.0.0:5000/")
.Build();
    }
  }
}
