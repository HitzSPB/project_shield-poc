using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TeamTwo.CloudShield.ShieldController;
using TeamTwo.CloudShield.ShieldController.Infrastructure;
using TeamTwo.CloudShield.ShieldController.Infrastructure.Mappers;
using TeamTwo.CloudShield.ShieldController.Services;
using TeamTwo.CloudShield.ShieldController.Utilities;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TeamTwo.CloudShield.ShieldController
{
  internal class Startup : FunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      builder.Services.AddTransient<IShieldInformationService, ShieldInformationService>();
      builder.Services.AddTransient<ICustomerManagementApiClient, CustomerManagementApiClient>();
      builder.Services.AddTransient<IShieldApiClient, ShieldApiClient>();
      builder.Services.AddTransient<IApplicationSettingsService, ApplicationSettingsService>();
      builder.Services.AddTransient<IHybridConnectionMapper, HybridConnectionMapper>();
    }

  }
}
