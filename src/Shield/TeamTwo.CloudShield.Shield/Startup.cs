using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TeamTwo.CloudShield.Shield;
using TeamTwo.CloudShield.Shield.Services;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TeamTwo.CloudShield.Shield
{
  internal class Startup : FunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      builder.Services.AddTransient<IProxyRelayCallService, ProxyRelayCallService>();
    }
  }
}
