using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TeamTwo.CloudShield.Shield;
using TeamTwo.CloudShield.Shield.Infrastructures;
using TeamTwo.CloudShield.Shield.Infrastructures.Mappers;
using TeamTwo.CloudShield.Shield.Services;
using TeamTwo.CloudShield.Shield.Utilities;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TeamTwo.CloudShield.Shield
{
  internal class Startup : FunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      builder.Services.AddTransient<IProxyRelayCallService, ProxyRelayCallService>();
      builder.Services.AddTransient<ICloudProviderHandlerApiClient, CloudProviderHandlerApiClient>();
      builder.Services.AddTransient<IRelayManagementService, RelayManagementService>();
      builder.Services.AddTransient<IStorageApiClient, StorageApiClient>();
      builder.Services.AddTransient<IApplicationsSettingsService, ApplicationsSettingsService>();
      builder.Services.AddTransient<IHybridConnectionDtoMapper, HybridConnectionDtoMapper>();
      builder.Services.AddTransient<IRelayApiClient, RelayApiClient>();

      builder.Services.AddHttpClient<ICloudProviderHandlerApiClient, CloudProviderHandlerApiClient>();
      builder.Services.AddHttpClient<IRelayApiClient, RelayApiClient>();
    }
  }
}
