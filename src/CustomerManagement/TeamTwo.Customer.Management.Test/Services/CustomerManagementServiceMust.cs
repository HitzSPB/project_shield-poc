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
using TeamTwo.Customer.Management.Services;
using TeamTwo.Customer.Management.Services.Models;
using Xunit;

namespace TeamTwo.Customer.Management.Test.Services
{
  public class CustomerManagementServiceMust
  {
    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ReturnCustomerInfoObjectWhenGetCustomerIsCalledAsync()
    {
      // Arrange
      ICustomerManagementStorageApiClient customerManagementStorageApiClient = A.Fake<ICustomerManagementStorageApiClient>();
      ICustomerManagementService sut = new CustomerManagementService(customerManagementStorageApiClient);
      A.CallTo(() => customerManagementStorageApiClient.GetCustomerAsync(TestHelper.CustomerId)).Returns(TestHelper.GetCustomerInfoWithTestHelperDefaultValues());
      // Act
      CustomerInfo actual = await sut.GetCustomerInformationAsync(TestHelper.CustomerId);

      // Assert
      Assert.IsType<CustomerInfo>(actual);
      Assert.NotNull(actual);
    }
    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ReturnCustomerInfoWhenStoreCustomerInformationAsync()
    {
      // Arrange
      ICustomerManagementStorageApiClient customerManagementStorageApiClient = A.Fake<ICustomerManagementStorageApiClient>();
      ICustomerManagementService sut = new CustomerManagementService(customerManagementStorageApiClient);
      A.CallTo(() => customerManagementStorageApiClient.StoreCustomerAsync(TestHelper.GetCustomerInfoWithTestHelperDefaultValues())).Returns(TestHelper.GetCustomerInfoWithTestHelperDefaultValues());
      // Act
      CustomerInfo actual = await sut.GetCustomerInformationAsync(TestHelper.CustomerId);

      // Assert
      Assert.NotNull(actual);
    }
  }
}
