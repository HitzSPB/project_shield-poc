﻿using System;
using System.Threading.Tasks;
using TeamTwo.Customer.Management.Infrastructure;
using TeamTwo.Customer.Management.Services.Models;

namespace TeamTwo.Customer.Management.Services
{
  public class CustomerManagementService : ICustomerManagementService
  {
    private readonly ICustomerManagementStorageApiClient _customerManagementStorageApiClient;
    public CustomerManagementService(ICustomerManagementStorageApiClient customerManagementStorageApiClient)
    {
      _customerManagementStorageApiClient = customerManagementStorageApiClient;
    }
    async Task<CustomerInfo> ICustomerManagementService.GetCustomerInformationAsync(Guid customerId)
    {
      return await _customerManagementStorageApiClient.GetCustomerAsync(customerId);
    }

    async Task<CustomerInfo> ICustomerManagementService.StoreCustomerInformationAsync(Guid customerId)
    {
      var customer = new CustomerInfo(customerId, Guid.NewGuid());
      return await _customerManagementStorageApiClient.StoreCustomerAsync(customer);
    }
  }
}
