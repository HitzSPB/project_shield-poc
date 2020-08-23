using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TeamTwo.Customer.Management.Apis;
using TeamTwo.Customer.Management.Apis.Models;
using TeamTwo.Customer.Management.Infrastructure;
using TeamTwo.Customer.Management.Infrastructure.Mappers;
using TeamTwo.Customer.Management.Infrastructure.Models;
using TeamTwo.Customer.Management.Services;
using TeamTwo.Customer.Management.Services.Models;
using Xunit;

namespace TeamTwo.Customer.Management.Test.Infrastructure.Mappers
{
  public class CustomerInfoMapperMust
  {
    [Fact]
    [Trait("Category", "UnitTest")]
    public void MapToCustomerInfoMustReturnCorrectValues()
    {
      // Arrange
      ICustomerInfoMapper sut = new CustomerInfoMapper();
      var customerInfoEntity = new CustomerInfoEntity(TestHelper.CustomerId, TestHelper.TenantId);

      // Act
      CustomerInfo actual = sut.MapToCustomerInfo(customerInfoEntity);

      //Assert
      Assert.Equal(customerInfoEntity.TenantId, actual.TenantId);
      Assert.Equal(customerInfoEntity.CustomerId, actual.CustomerId);

    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void MapToCustomerInfoThrowsArgumentNullExceptionIfObjectIsNull()
    {
      // Arrange
      ICustomerInfoMapper sut = new CustomerInfoMapper();
      CustomerInfoEntity customerInfoEntity = null;

      // Act & Assert
      Assert.Throws<ArgumentNullException>(() => sut.MapToCustomerInfo(customerInfoEntity));

    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void MapToCustomerInfoStorageDtoReturnsCorrectValues()
    {
      // Arrange
      ICustomerInfoMapper sut = new CustomerInfoMapper();
      var customerInfo = new CustomerInfo(TestHelper.CustomerId, TestHelper.TenantId);

      // Act
      CustomerInfoEntity actual = sut.MapToCustomerInfoStorageDto(customerInfo);

      //Assert
      Assert.Equal(customerInfo.TenantId, actual.TenantId);
      Assert.Equal(customerInfo.CustomerId, actual.CustomerId);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void MapToCustomerInfoStorageDtoThrowsArgumentNullExceptionIfObjectIsNull()
    {
      // Arrange
      ICustomerInfoMapper sut = new CustomerInfoMapper();
      CustomerInfo customerInfo = null;

      // Act & Assert
      Assert.Throws<ArgumentNullException>(() => sut.MapToCustomerInfoStorageDto(customerInfo));
    }
  }
}
