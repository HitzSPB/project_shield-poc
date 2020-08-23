using System;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
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
      IRelayAzureManagementService sut = new RelayAzureManagementService(relayManagementApiClient);
      var tenantId = TestHelper.TenantId;
      var createRelayDto = new CreateRelayDto() { TenantId = tenantId };
      A.CallTo(() => relayManagementApiClient.CreateHybridConnectionAsync(tenantId)).Returns(TestHelper.GetDefaultLocalHostUri);
      A.CallTo(() => relayManagementApiClient.CreatePolicykeyAsync(tenantId, PolicyClaim.Listen)).Returns(TestHelper.GetListenPolicyDto());
      A.CallTo(() => relayManagementApiClient.CreatePolicykeyAsync(tenantId, PolicyClaim.Send)).Returns(TestHelper.GetSendPolicyDto());

      // Act
      HybridConnectionDto actual = await sut.CreateHybridConnection(createRelayDto);

      // Assert
      Assert.Equal(TestHelper.GetDefaultLocalHostUri, actual.HybridConnectionUrl);
      Assert.Equal(TestHelper.GetListenPolicyDto().PolicyKey, actual.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Listen).PolicyKey);
      Assert.Equal(TestHelper.GetSendPolicyDto().PolicyKey, actual.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Send).PolicyKey);
      Assert.Equal(TestHelper.GetListenPolicyDto().PolicyName, actual.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Listen).PolicyName);
      Assert.Equal(TestHelper.GetSendPolicyDto().PolicyName, actual.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Send).PolicyName);
      Assert.Equal(TestHelper.GetListenPolicyDto().PolicyType, actual.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Listen).PolicyType);
      Assert.Equal(TestHelper.GetSendPolicyDto().PolicyType, actual.PolicyDtos.FirstOrDefault(x => x.PolicyType == PolicyClaim.Send).PolicyType);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowArgumentNullExceptionWhenMethodInputIsNullAsync()
    {
      // Arrange
      IRelayManagementApiClient relayManagementApiClient = A.Fake<IRelayManagementApiClient>();
      IRelayAzureManagementService sut = new RelayAzureManagementService(relayManagementApiClient);

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => sut.CreateHybridConnection(null));
    }
  }
}
