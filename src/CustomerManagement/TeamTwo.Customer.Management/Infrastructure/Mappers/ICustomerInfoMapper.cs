using TeamTwo.Customer.Management.Infrastructure.Models;
using TeamTwo.Customer.Management.Services.Models;

namespace TeamTwo.Customer.Management.Infrastructure.Mappers
{
  public interface ICustomerInfoMapper
  {
    CustomerInfo MapToCustomerInfo(CustomerInfoEntity customerInfoStorageEntity);
    CustomerInfoEntity MapToCustomerInfoStorageDto(CustomerInfo customerInfo);
  }
}