using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TeamTwo.Customer.Management;
using TeamTwo.Customer.Management.Infrastructure;
using TeamTwo.Customer.Management.Infrastructure.Mappers;
using TeamTwo.Customer.Management.Services;
using TeamTwo.Customer.Management.Utilities;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TeamTwo.Customer.Management
{
  internal class Startup : FunctionsStartup
  {
    public override void Configure(IFunctionsHostBuilder builder)
    {
      builder.Services.AddTransient<ICustomerManagementService, CustomerManagementService>();
      builder.Services.AddTransient<ICustomerManagementStorageApiClient, CustomerManagementStorageApiClient>();
      builder.Services.AddTransient<IApplicationSettingsService, ApplicationSettingsService>();
      builder.Services.AddTransient<ICustomerInfoMapper, CustomerInfoMapper>();
    }

  }
}
