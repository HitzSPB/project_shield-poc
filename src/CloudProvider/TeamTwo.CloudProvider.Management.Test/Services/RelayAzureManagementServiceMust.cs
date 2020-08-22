using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using TeamTwo.CloudProvider.Management.Infrastructure;
using TeamTwo.CloudProvider.Management.Services;
using TeamTwo.CloudProvider.Management.Services.Models;
using Xunit;


namespace TeamTwo.CloudProvider.Management.Test.Services
{
  public class RelayAzureManagementServiceMust
  {
    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ReturnHybridConnectionDtoWhenCreateHybridConnectionIsCalledAsync()
    {
      // Arrange
      IRelayManagementApiClient relayManagementApiClient = A.Fake<IRelayManagementApiClient>();
      IRelayAzureManagementService relayAzureManagementService = new RelayAzureManagementService(relayManagementApiClient);
      var tenantId = TestHelper.TenantId;
      var createRelayDto = new CreateRelayDto() { TenantId = tenantId };
      A.CallTo(() => relayManagementApiClient.CreateHybridConnectionAsync(tenantId)).Returns(TestHelper.GetDefaultLocalHostUri);
      A.CallTo(() => relayManagementApiClient.CreatePolicykeyAsync(tenantId, PolicyClaim.Listen)).Returns(TestHelper.GetListenPolicyDto());
      A.CallTo(() => relayManagementApiClient.CreatePolicykeyAsync(tenantId, PolicyClaim.Send)).Returns(TestHelper.GetSendPolicyDto());

      // Act
      HybridConnectionDto response = await relayAzureManagementService.CreateHybridConnection(createRelayDto);

      // Assert
      Assert.Equal(TestHelper.GetDefaultLocalHostUri, response.HybridConnectionUrl);
      Assert.Equal(TestHelper.GetListenPolicyDto().PolicyKey, response.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Listen).PolicyKey);
      Assert.Equal(TestHelper.GetSendPolicyDto().PolicyKey, response.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Send).PolicyKey);
      Assert.Equal(TestHelper.GetListenPolicyDto().PolicyName, response.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Listen).PolicyName);
      Assert.Equal(TestHelper.GetSendPolicyDto().PolicyName, response.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Send).PolicyName);
      Assert.Equal(TestHelper.GetListenPolicyDto().PolicyType, response.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Listen).PolicyType);
      Assert.Equal(TestHelper.GetSendPolicyDto().PolicyType, response.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Send).PolicyType);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowArgumentNullExceptionWhenMethodInputIsNullAsync()
    {
      // Arrange
      IRelayManagementApiClient relayManagementApiClient = A.Fake<IRelayManagementApiClient>();
      IRelayAzureManagementService relayAzureManagementService = new RelayAzureManagementService(relayManagementApiClient);

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => relayAzureManagementService.CreateHybridConnection(null));
    }
  }
}
