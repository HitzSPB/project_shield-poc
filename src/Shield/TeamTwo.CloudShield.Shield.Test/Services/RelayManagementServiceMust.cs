using System;
using System.Threading.Tasks;
using FakeItEasy;
using TeamTwo.CloudShield.Shield.Infrastructures;
using TeamTwo.CloudShield.Shield.Service.Models;
using TeamTwo.CloudShield.Shield.Services;
using Xunit;

namespace TeamTwo.CloudShield.Shield.Test.Services
{
  public class RelayManagementServiceMust
  {
    [Fact]
    public async Task ReturnHybridConnectionDtoWhenGetRelayIsCalledAsync()
    {
      // Arrange
      IStorageApiClient storageApiClient = A.Fake<IStorageApiClient>();
      ICloudProviderHandlerApiClient cloudProviderHandlerApiClient = A.Fake<ICloudProviderHandlerApiClient>();
      IRelayManagementService sut = new RelayManagementService(storageApiClient, cloudProviderHandlerApiClient);
      A.CallTo(() => storageApiClient.GetRelayFromIdAsync(TestHelper.TenantId.ToString())).Returns(TestHelper.GetHybridConnectionDto());
      // Act
      HybridConnectionDto actual = await sut.GetRelayAsync(TestHelper.TenantId.ToString());

      // Assert
      Assert.Equal(TestHelper.GetHybridConnectionDto().HybridConnectionUrl, actual.HybridConnectionUrl);

    }
    [Fact]
    public async Task ReturnHybridConnectionDtoWhenStoreRelayIsCalledAsync()
    {
      // Arrange
      IStorageApiClient storageApiClient = A.Fake<IStorageApiClient>();
      ICloudProviderHandlerApiClient cloudProviderHandlerApiClient = A.Fake<ICloudProviderHandlerApiClient>();
      IRelayManagementService sut = new RelayManagementService(storageApiClient, cloudProviderHandlerApiClient);
      A.CallTo(() => cloudProviderHandlerApiClient.CreateRelayHybridConnection(TestHelper.TenantId.ToString())).WithAnyArguments().Returns(TestHelper.GetHybridConnectionDto());
      A.CallTo(() => storageApiClient.StoreRelayAsync(TestHelper.TenantId.ToString(), TestHelper.GetHybridConnectionDto())).WithAnyArguments().Returns(TestHelper.GetHybridConnectionDto());
      // Act
      HybridConnectionDto actual = await sut.StoreRelayAsync(TestHelper.GetCreateRelayStorageDto());

      // Assert
      Assert.Equal(TestHelper.GetHybridConnectionDto().HybridConnectionUrl, actual.HybridConnectionUrl);

    }

    [Fact]
    public async Task ThrowInvalidOperationExceptionWhenCloudProviderDidNotReturnHybridConnectionAsync()
    {
      // Arrange
      IStorageApiClient storageApiClient = A.Fake<IStorageApiClient>();
      ICloudProviderHandlerApiClient cloudProviderHandlerApiClient = A.Fake<ICloudProviderHandlerApiClient>();
      IRelayManagementService sut = new RelayManagementService(storageApiClient, cloudProviderHandlerApiClient);
      A.CallTo(() => cloudProviderHandlerApiClient.CreateRelayHybridConnection(TestHelper.TenantId.ToString())).WithAnyArguments().Returns<HybridConnectionDto>(null);
      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => sut.StoreRelayAsync(TestHelper.GetCreateRelayStorageDto()));

    }
  }
}
