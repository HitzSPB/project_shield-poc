using System;
using System.Collections.Generic;
using System.Text;
using TeamTwo.Customer.Management.Infrastructure.Models;
using TeamTwo.Customer.Management.Services.Models;

namespace TeamTwo.Customer.Management.Infrastructure.Mappers
{
  public class CustomerInfoMapper : ICustomerInfoMapper
  {

    CustomerInfo ICustomerInfoMapper.MapToCustomerInfo(CustomerInfoEntity customerInfoStorageEntity)
    {
      return new CustomerInfo(customerInfoStorageEntity.CustomerId, customerInfoStorageEntity.TenantId);
    }
    CustomerInfoEntity ICustomerInfoMapper.MapToCustomerInfoStorageDto(CustomerInfo customerInfo)
    {
      return new CustomerInfoEntity(customerInfo.CustomerId, customerInfo.TenantId);
    }
  }
}
