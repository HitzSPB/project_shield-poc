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
using TeamTwo.Customer.Management.Services;
using TeamTwo.Customer.Management.Services.Models;
using Xunit;

namespace TeamTwo.Customer.Management.Test
{
  public class CustomerManagementFunctionsMust
  {
    [Fact]
    [Trait("Category","UnitTest")]
    public async Task BeAbleToGetCustomerAsync()
    {
      // Arrange
      ICustomerManagementService customerManagementService = A.Fake<ICustomerManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new CustomerManagementFunctions(customerManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);
      A.CallTo(() => customerManagementService.GetCustomerInformationAsync(TestHelper.CustomerId))
        .Returns(TestHelper.GetCustomerInfoWithTestHelperDefaultValues());

      // Act
      IActionResult actual = await sut.GetCustomerAsync(httpRequest, TestHelper.CustomerId.ToString(), logger);

      // Assert
      Assert.Equal(HttpStatusCode.OK, (HttpStatusCode) ((ObjectResult) actual).StatusCode);
      Assert.Equal(TestHelper.GetCustomerInfoWithTestHelperDefaultValues().CustomerId, ((CustomerInfo)((ObjectResult) actual).Value).CustomerId);
      Assert.Equal(TestHelper.GetCustomerInfoWithTestHelperDefaultValues().TenantId, ((CustomerInfo) ((ObjectResult) actual).Value).TenantId);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task GetBadRequestIfGetCustomerCallDoesNotContainValidGuidAsCustomerIdAsync()
    {
      // Arrange
      ICustomerManagementService customerManagementService = A.Fake<ICustomerManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new CustomerManagementFunctions(customerManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);

      // Act
      IActionResult actual = await sut.GetCustomerAsync(httpRequest, "123434", logger);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode) ((ObjectResult) actual).StatusCode);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task BeAbleToStoreCustomerAsync()
    {
      // Arrange
      ICustomerManagementService customerManagementService = A.Fake<ICustomerManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new CustomerManagementFunctions(customerManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext)
      {
        Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(TestHelper.GetStoreCustomerWithTestHelperDefaultValues())))
      };
      A.CallTo(() => customerManagementService.StoreCustomerInformationAsync(TestHelper.CustomerId))
        .Returns(TestHelper.GetCustomerInfoWithTestHelperDefaultValues());

      // Act
      IActionResult actual = await sut.StoreCustomerAsync(httpRequest);

      // Assert
      Assert.Equal(HttpStatusCode.OK, (HttpStatusCode) ((ObjectResult) actual).StatusCode);
      Assert.Equal(TestHelper.GetCustomerInfoWithTestHelperDefaultValues().CustomerId, ((CustomerInfo) ((ObjectResult) actual).Value).CustomerId);
      Assert.Equal(TestHelper.GetCustomerInfoWithTestHelperDefaultValues().TenantId, ((CustomerInfo) ((ObjectResult) actual).Value).TenantId);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task BeGetBadRequestWhenStoreCustomerIsCalledWithoutBodyAsync()
    {
      // Arrange
      ICustomerManagementService customerManagementService = A.Fake<ICustomerManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new CustomerManagementFunctions(customerManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);
      A.CallTo(() => customerManagementService.StoreCustomerInformationAsync(TestHelper.CustomerId))
        .Returns(TestHelper.GetCustomerInfoWithTestHelperDefaultValues());

      // Act
      IActionResult actual = await sut.StoreCustomerAsync(httpRequest);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode) ((StatusCodeResult) actual).StatusCode);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowsExceptionWhenBodyIsNotCorrectFormatAsync()
    {
      // Arrange
      ICustomerManagementService customerManagementService = A.Fake<ICustomerManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new CustomerManagementFunctions(customerManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext)
      {
        Body = new MemoryStream(Encoding.UTF8.GetBytes("NotCorrectFormat"))
      };
      A.CallTo(() => customerManagementService.StoreCustomerInformationAsync(TestHelper.CustomerId))
        .Returns(TestHelper.GetCustomerInfoWithTestHelperDefaultValues());

      // Act & Assert
      await Assert.ThrowsAsync<JsonReaderException>(() => sut.StoreCustomerAsync(httpRequest));
    }
  }
}
