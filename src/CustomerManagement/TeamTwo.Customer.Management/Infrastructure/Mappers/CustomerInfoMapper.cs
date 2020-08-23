using System;
using TeamTwo.Customer.Management.Infrastructure.Models;
using TeamTwo.Customer.Management.Services.Models;

namespace TeamTwo.Customer.Management.Infrastructure.Mappers
{
  public class CustomerInfoMapper : ICustomerInfoMapper
  {

    CustomerInfo ICustomerInfoMapper.MapToCustomerInfo(CustomerInfoEntity customerInfoStorageEntity)
    {
      if (customerInfoStorageEntity is null) throw new ArgumentNullException(nameof(customerInfoStorageEntity));
      return new CustomerInfo(customerInfoStorageEntity.CustomerId, customerInfoStorageEntity.TenantId);
    }
    CustomerInfoEntity ICustomerInfoMapper.MapToCustomerInfoStorageDto(CustomerInfo customerInfo)
    {
      if (customerInfo is null) throw new ArgumentNullException(nameof(customerInfo));
      return new CustomerInfoEntity(customerInfo.CustomerId, customerInfo.TenantId);
    }
  }
}
