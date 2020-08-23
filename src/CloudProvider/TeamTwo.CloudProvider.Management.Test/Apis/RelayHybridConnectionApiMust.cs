using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TeamTwo.CloudProvider.Management.Services;
using TeamTwo.CloudProvider.Management.Services.Models;
using Xunit;

namespace TeamTwo.CloudProvider.Management.Test.Apis
{
  public class RelayHybridConnectionApiMust
  {
    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ReturnObjecResultsWithHybridConnectionDtoAndStatusCodeOkAsync()
    {
      // Arrange
      IRelayAzureManagementService relayAzureManagementService = A.Fake<IRelayAzureManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new RelayHybridConnectionApi(relayAzureManagementService);
      var httpContext = new DefaultHttpContext();
      var createRelayStorageDto = new CreateRelayDto() { TenantId = TestHelper.TenantId };
      var httpRequest = new DefaultHttpRequest(httpContext)
      { Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(createRelayStorageDto))) };
      var hybridConnectionDto = new HybridConnectionDto(TestHelper.GetDefaultLocalHostUri, new PolicyDto[2]
      {
        TestHelper.GetListenPolicyDto(),
        TestHelper.GetSendPolicyDto()
      });

      A.CallTo(() => relayAzureManagementService.CreateHybridConnection(createRelayStorageDto)).WithAnyArguments().Returns(hybridConnectionDto);

      // Act
      IActionResult actual = await sut.CreateRelayHybridConnectionAsync(httpRequest, logger);

      // Assert
      Assert.Equal(HttpStatusCode.OK, (HttpStatusCode) ((ObjectResult) actual).StatusCode);
      Assert.Equal(hybridConnectionDto, (HybridConnectionDto) ((ObjectResult) actual).Value);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ReturnBadRequestWhenServiceCallReturnsNullAsync()
    {
      // Arrange
      IRelayAzureManagementService relayAzureManagementService = A.Fake<IRelayAzureManagementService>();
      ILogger logger = A.Fake<ILogger>();
      var sut = new RelayHybridConnectionApi(relayAzureManagementService);
      var httpContext = new DefaultHttpContext();
      var httpRequest = new DefaultHttpRequest(httpContext);
      A.CallTo(() => relayAzureManagementService.CreateHybridConnection(null)).Returns<HybridConnectionDto>(null);

      // Act
      IActionResult actual = await sut.CreateRelayHybridConnectionAsync(httpRequest, logger);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, (HttpStatusCode) ((StatusCodeResult) actual).StatusCode);
    }
  }
}
