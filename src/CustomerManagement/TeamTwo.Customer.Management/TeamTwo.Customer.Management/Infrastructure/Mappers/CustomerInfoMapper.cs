using System;
using System.Collections.Generic;
using System.Text;
using TeamTwo.Customer.Management.Infrastructure.Models;
using TeamTwo.Customer.Management.Services.Models;

namespace TeamTwo.Customer.Management.Infrastructure.Mappers
{
  public class CustomerInfoMapper : ICustomerInfoMapper
  {

    CustomerInfo ICustomerInfoMapper.MapToCustomerInfo(CustomerInfoStorageDto customerInfoStorageDto)
    {
      return new CustomerInfo(customerInfoStorageDto.CustomerId, customerInfoStorageDto.TenantId);
    }
    CustomerInfoStorageDto ICustomerInfoMapper.MapToCustomerInfoStorageDto(CustomerInfo customerInfo)
    {
      return new CustomerInfoStorageDto(customerInfo.CustomerId, customerInfo.TenantId);
    }
  }
}
