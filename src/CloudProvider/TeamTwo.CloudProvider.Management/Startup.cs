using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TeamTwo.CloudProvider.Management;
using TeamTwo.CloudProvider.Management.Infrastructure;
using TeamTwo.CloudProvider.Management.Services;
using TeamTwo.CloudProvider.Management.Utilities;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TeamTwo.CloudProvider.Management
{
  [ExcludeFromCodeCoverage]
  internal class Startup : FunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      builder.Services.AddTransient<IRelayManagementApiClient, RelayManagementApiClient>();
      builder.Services.AddTransient<IRelayAzureManagementService, RelayAzureManagementService>();
      builder.Services.AddTransient<IApplicationSettingsService, ApplicationSettingsService>();
      builder.Services.AddTransient<AzureAuthHandler>();

      builder.Services.AddHttpClient<IRelayManagementApiClient, RelayManagementApiClient>().AddHttpMessageHandler<AzureAuthHandler>();
    }
  }
}
