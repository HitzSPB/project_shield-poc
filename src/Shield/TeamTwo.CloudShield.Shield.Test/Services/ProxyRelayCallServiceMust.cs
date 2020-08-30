using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using TeamTwo.CloudShield.Shield.Infrastructures;
using TeamTwo.CloudShield.Shield.Service.Models;
using TeamTwo.CloudShield.Shield.Services;
using Xunit;

namespace TeamTwo.CloudShield.Shield.Test.Services
{
  public class ProxyRelayCallServiceMust
  {
    [Fact]
    public async Task ReturnStatusCodeOkayWhenProxyRelayWasSuccessfulAsync()
    {
      // Arrange 
      IStorageApiClient storageApiClient = A.Fake<IStorageApiClient>();
      IRelayApiClient relayApiClient = A.Fake<IRelayApiClient>();
      IProxyRelayCallService sut = new ProxyRelayCallService(storageApiClient, relayApiClient);
      A.CallTo(() => relayApiClient.RelayCallAsync(new RelayCallDto(TestHelper.GetHybridConnectionDto(), "body", new HttpMethod(HttpMethods.Post), null, "test/endpoint")))
        .Returns(new HttpResponseMessage(HttpStatusCode.OK));
      // Act
      HttpResponseMessage actual = await sut.ProxyRelayCallAsync(TestHelper.TenantId.ToString(), "body", new HttpMethod(HttpMethods.Post), null, "test/endpoint");

      // Assert
      Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
    }
    [Fact]
    public async Task ReturnStatusCodeNotFoundWhenNoHybridConnectionWasFoundAsync()
    {
      // Arrange 
      IStorageApiClient storageApiClient = A.Fake<IStorageApiClient>();
      IRelayApiClient relayApiClient = A.Fake<IRelayApiClient>();
      IProxyRelayCallService sut = new ProxyRelayCallService(storageApiClient, relayApiClient);
      A.CallTo(() => relayApiClient.RelayCallAsync(new RelayCallDto(TestHelper.GetHybridConnectionDto(), "body", new HttpMethod(HttpMethods.Post), null, "test/endpoint")))
        .Returns(new HttpResponseMessage(HttpStatusCode.OK));
      A.CallTo(() => storageApiClient.GetRelayFromIdAsync(TestHelper.TenantId.ToString())).WithAnyArguments().Returns<HybridConnectionDto>(null);
      // Act
      HttpResponseMessage actual = await sut.ProxyRelayCallAsync(TestHelper.TenantId.ToString(), "body", new HttpMethod(HttpMethods.Post), null, "test/endpoint");

      // Assert
      Assert.Equal(HttpStatusCode.NotFound, actual.StatusCode);
    }
    [Fact]
    public async Task ThrowArgumentNullExceptionWhenNoTenantIdAsync()
    {
      // Arrange 
      IStorageApiClient storageApiClient = A.Fake<IStorageApiClient>();
      IRelayApiClient relayApiClient = A.Fake<IRelayApiClient>();
      IProxyRelayCallService sut = new ProxyRelayCallService(storageApiClient, relayApiClient);
      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => sut.ProxyRelayCallAsync("", "body", new HttpMethod(HttpMethods.Post), null, "test/endpoint"));
    }
    [Fact]
    public async Task ThrowArgumentNullExceptionWhenNoBodyIsPresentAsync()
    {
      // Arrange 
      IStorageApiClient storageApiClient = A.Fake<IStorageApiClient>();
      IRelayApiClient relayApiClient = A.Fake<IRelayApiClient>();
      IProxyRelayCallService sut = new ProxyRelayCallService(storageApiClient, relayApiClient);
      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => sut.ProxyRelayCallAsync(TestHelper.TenantId.ToString(), "", new HttpMethod(HttpMethods.Post), null, "test/endpoint"));
    }
    [Fact]
    public async Task ThrowArgumentNullExceptionWhenNoMethodIsPresentAsync()
    {
      // Arrange 
      IStorageApiClient storageApiClient = A.Fake<IStorageApiClient>();
      IRelayApiClient relayApiClient = A.Fake<IRelayApiClient>();
      IProxyRelayCallService sut = new ProxyRelayCallService(storageApiClient, relayApiClient);
      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => sut.ProxyRelayCallAsync(TestHelper.TenantId.ToString(), "body", null, null, "test/endpoint"));
    }
    [Fact]
    public async Task ThrowArgumentNullExceptionWhenNoUrlIsPresentAsync()
    {
      // Arrange 
      IStorageApiClient storageApiClient = A.Fake<IStorageApiClient>();
      IRelayApiClient relayApiClient = A.Fake<IRelayApiClient>();
      IProxyRelayCallService sut = new ProxyRelayCallService(storageApiClient, relayApiClient);
      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => sut.ProxyRelayCallAsync(TestHelper.TenantId.ToString(), "body", new HttpMethod(HttpMethods.Post), null, ""));
    }
  }
}
