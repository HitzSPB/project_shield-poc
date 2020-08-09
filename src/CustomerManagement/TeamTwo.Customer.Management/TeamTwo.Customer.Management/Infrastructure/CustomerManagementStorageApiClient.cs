using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using TeamTwo.Customer.Management.Infrastructure.Mappers;
using TeamTwo.Customer.Management.Infrastructure.Models;
using TeamTwo.Customer.Management.Services.Models;
using TeamTwo.Customer.Management.Utilities;

namespace TeamTwo.Customer.Management.Infrastructure
{
  public class CustomerManagementStorageApiClient : ICustomerManagementStorageApiClient
  {
    private readonly IApplicationSettingsService _applicationSettingsService;
    private readonly ICustomerInfoMapper _customerInfoMapper;
    private readonly CloudStorageAccount _account;
    public CustomerManagementStorageApiClient(ICustomerInfoMapper customerInfoMapper, IApplicationSettingsService applicationSettingsService)
    {
      _applicationSettingsService = applicationSettingsService;
      _customerInfoMapper = customerInfoMapper;
      _account = CloudStorageAccount.Parse(_applicationSettingsService.GetProccessEnvironmentVariable("AzureStorageAccountConnection"));
    }

    async Task<CustomerInfo> ICustomerManagementStorageApiClient.GetCustomerAsync(Guid tenantId)
    {
      CloudTable table = await SetupCloudTableAsync();
      var tableOperation = TableOperation.Retrieve<CustomerInfoEntity>(tenantId.ToString(), tenantId.ToString());
      TableResult tableOpreationResult = await table.ExecuteAsync(tableOperation);
      var result = tableOpreationResult.Result as CustomerInfoEntity;
      return _customerInfoMapper.MapToCustomerInfo(result);
    }

    async Task<CustomerInfo> ICustomerManagementStorageApiClient.StoreCustomerAsync(CustomerInfo customer)
    {
      CloudTable table = await SetupCloudTableAsync();
      var insertOperation = TableOperation.Insert(_customerInfoMapper.MapToCustomerInfoStorageDto(customer));
      TableResult tableOpreationResult = await table.ExecuteAsync(insertOperation);
      var result = tableOpreationResult.Result as CustomerInfoEntity;
      return _customerInfoMapper.MapToCustomerInfo(result);
    }
    private async Task<CloudTable> SetupCloudTableAsync()
    {
      CloudTableClient client = _account.CreateCloudTableClient();
      CloudTable table = client.GetTableReference("CustomerInformation");
      await table.CreateIfNotExistsAsync();
      return table;
    }
  }
}
