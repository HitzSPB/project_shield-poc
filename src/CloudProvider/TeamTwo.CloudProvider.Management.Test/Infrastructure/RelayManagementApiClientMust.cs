using System;
using System.Collections.Generic;
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
using TeamTwo.CloudProvider.Management.Infrastructure;
using TeamTwo.CloudProvider.Management.Services;
using TeamTwo.CloudProvider.Management.Services.Models;
using TeamTwo.CloudProvider.Management.Test.Mocks;
using TeamTwo.CloudProvider.Management.Utilities;
using Xunit;


namespace TeamTwo.CloudProvider.Management.Test.Infrastructure
{
  public class RelayManagementApiClientMust
  {
    #region CreateHybridConnectionRegion
    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task BeAbleToCreateHybridConnectionAsync()
    {
      // Arrange
      IApplicationSettingsService applicationSettingsService = A.Fake<IApplicationSettingsService>();
      var relayNameSpace = "Relay";
      A.CallTo(() => applicationSettingsService.AzureManagementApi).Returns(TestHelper.GetDefaultLocalHostUri.ToString());
      A.CallTo(() => applicationSettingsService.AzureSubscriptionId).Returns("ffc1f3f6-a8ac-4d89-b485-02563edf182g");
      A.CallTo(() => applicationSettingsService.ResourceGroupname).Returns("Relay_RG");
      A.CallTo(() => applicationSettingsService.RelayNameSpace).Returns(relayNameSpace);

      IRelayManagementApiClient relayManagementApiClient = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act
      Uri response = await relayManagementApiClient.CreateHybridConnectionAsync(TestHelper.TenantId);

      // Assert
      Assert.Contains(TestHelper.TenantId, response.OriginalString);
      Assert.Contains(relayNameSpace, response.OriginalString);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowHttpExceptionIfHttpRequestDoesNotReturnOkAsync()
    {
      // Arrange
      IApplicationSettingsService applicationSettingsService = A.Fake<IApplicationSettingsService>();
      var relayNameSpace = "Relay";
      A.CallTo(() => applicationSettingsService.AzureManagementApi).Returns(TestHelper.GetCorrectUrlFormatWithWrongAddress.ToString());
      A.CallTo(() => applicationSettingsService.AzureSubscriptionId).Returns("ffc1f3f6-a8ac-4d89-b485-02563edf182g");
      A.CallTo(() => applicationSettingsService.ResourceGroupname).Returns("Relay_RG");
      A.CallTo(() => applicationSettingsService.RelayNameSpace).Returns(relayNameSpace);

      IRelayManagementApiClient relayManagementApiClient = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<HttpRequestException>(() => relayManagementApiClient.CreateHybridConnectionAsync(TestHelper.TenantId));
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void ThrowExceptionWhenAzureManagementApiUriIsNotCorrectFormat()
    {
      // Arrange
      IApplicationSettingsService applicationSettingsService = A.Fake<IApplicationSettingsService>();
      var relayNameSpace = "Relay";
      A.CallTo(() => applicationSettingsService.AzureManagementApi).Returns("NotAUriFormatString");
      A.CallTo(() => applicationSettingsService.AzureSubscriptionId).Returns("ffc1f3f6-a8ac-4d89-b485-02563edf182g");
      A.CallTo(() => applicationSettingsService.ResourceGroupname).Returns("Relay_RG");
      A.CallTo(() => applicationSettingsService.RelayNameSpace).Returns(relayNameSpace);
      var didCatchException = false;

      // Act & Assert
      try
      {
      IRelayManagementApiClient relayManagementApiClient = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));
      }
      catch(UriFormatException exception)
      {
        didCatchException = true;
        Assert.IsType<UriFormatException>(exception);
      }

      Assert.True(didCatchException);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void ThrowExceptionWhenAzureManagementApiUriIsNotPresent()
    {
      // Arrange
      IApplicationSettingsService applicationSettingsService = A.Fake<IApplicationSettingsService>();
      var relayNameSpace = "Relay";
      A.CallTo(() => applicationSettingsService.AzureSubscriptionId).Returns("ffc1f3f6-a8ac-4d89-b485-02563edf182g");
      A.CallTo(() => applicationSettingsService.ResourceGroupname).Returns("Relay_RG");
      A.CallTo(() => applicationSettingsService.RelayNameSpace).Returns(relayNameSpace);

      var didCatchException = false;

      // Act & Assert
      try
      {
        IRelayManagementApiClient relayManagementApiClient = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));
      }
      catch (InvalidOperationException exception)
      {
        didCatchException = true;
        Assert.IsType<InvalidOperationException>(exception);
      }

      Assert.True(didCatchException);
    }


    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowExceptionWhenAzureSubscriptionIdIsNotPresentAsync()
    {
      // Arrange
      IApplicationSettingsService applicationSettingsService = A.Fake<IApplicationSettingsService>();
      var relayNameSpace = "Relay";
      A.CallTo(() => applicationSettingsService.AzureManagementApi).Returns(TestHelper.GetDefaultLocalHostUri.ToString());
      A.CallTo(() => applicationSettingsService.ResourceGroupname).Returns("Relay_RG");
      A.CallTo(() => applicationSettingsService.RelayNameSpace).Returns(relayNameSpace);

      IRelayManagementApiClient relayManagementApiClient = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => relayManagementApiClient.CreateHybridConnectionAsync(TestHelper.TenantId));
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowExceptionWhenResourceGroupnameIsNotPresentAsync()
    {
      // Arrange
      IApplicationSettingsService applicationSettingsService = A.Fake<IApplicationSettingsService>();
      var relayNameSpace = "Relay";
      A.CallTo(() => applicationSettingsService.AzureManagementApi).Returns(TestHelper.GetDefaultLocalHostUri.ToString());
      A.CallTo(() => applicationSettingsService.AzureSubscriptionId).Returns("ffc1f3f6-a8ac-4d89-b485-02563edf182g");
      A.CallTo(() => applicationSettingsService.RelayNameSpace).Returns(relayNameSpace);

      IRelayManagementApiClient relayManagementApiClient = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => relayManagementApiClient.CreateHybridConnectionAsync(TestHelper.TenantId));
    }
    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowExceptionWhenRelayNameSpaceIsNotPresentAsync()
    {
      // Arrange
      IApplicationSettingsService applicationSettingsService = A.Fake<IApplicationSettingsService>();
      A.CallTo(() => applicationSettingsService.AzureManagementApi).Returns(TestHelper.GetDefaultLocalHostUri.ToString());
      A.CallTo(() => applicationSettingsService.AzureSubscriptionId).Returns("ffc1f3f6-a8ac-4d89-b485-02563edf182g");
      A.CallTo(() => applicationSettingsService.ResourceGroupname).Returns("Relay_RG");

      IRelayManagementApiClient relayManagementApiClient = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => relayManagementApiClient.CreateHybridConnectionAsync(TestHelper.TenantId));
    }
    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowExceptionWhenTenantIdIsNotPresentAsync()
    {
      // Arrange
      IApplicationSettingsService applicationSettingsService = A.Fake<IApplicationSettingsService>();
      var relayNameSpace = "Relay";
      A.CallTo(() => applicationSettingsService.AzureManagementApi).Returns(TestHelper.GetDefaultLocalHostUri.ToString());
      A.CallTo(() => applicationSettingsService.AzureSubscriptionId).Returns("ffc1f3f6-a8ac-4d89-b485-02563edf182g");
      A.CallTo(() => applicationSettingsService.ResourceGroupname).Returns("Relay_RG");
      A.CallTo(() => applicationSettingsService.RelayNameSpace).Returns(relayNameSpace);

      IRelayManagementApiClient relayManagementApiClient = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => relayManagementApiClient.CreateHybridConnectionAsync(""));
    }

    #endregion
    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task BeAbleToCreateHybridConnectionPolicyAsync()
    {
      // Arrange
      IApplicationSettingsService applicationSettingsService = A.Fake<IApplicationSettingsService>();
      var relayNameSpace = "Relay";
      A.CallTo(() => applicationSettingsService.AzureManagementApi).Returns(TestHelper.GetDefaultLocalHostUri.ToString());
      A.CallTo(() => applicationSettingsService.AzureSubscriptionId).Returns("ffc1f3f6-a8ac-4d89-b485-02563edf182g");
      A.CallTo(() => applicationSettingsService.ResourceGroupname).Returns("Relay_RG");
      A.CallTo(() => applicationSettingsService.RelayNameSpace).Returns(relayNameSpace);

      IRelayManagementApiClient relayManagementApiClient = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act
      PolicyDto response = await relayManagementApiClient.CreatePolicykeyAsync(TestHelper.TenantId, PolicyClaim.Listen);

      // Assert
      Assert.NotNull(response);
      Assert.Equal(TestHelper.PrimaryKey, response.PolicyKey);
      Assert.True(Guid.TryParse(response.PolicyName, out Guid policyNameAsGuid));
      Assert.Equal(PolicyClaim.Listen, response.PolicyType);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowHttpExceptionIfPolicyKeyHttpRequestDoesNotReturnOkAsync()
    {
      // Arrange
      IApplicationSettingsService applicationSettingsService = A.Fake<IApplicationSettingsService>();
      var relayNameSpace = "Relay";
      A.CallTo(() => applicationSettingsService.AzureManagementApi).Returns(TestHelper.GetCorrectUrlFormatWithWrongAddress.ToString());
      A.CallTo(() => applicationSettingsService.AzureSubscriptionId).Returns("ffc1f3f6-a8ac-4d89-b485-02563edf182g");
      A.CallTo(() => applicationSettingsService.ResourceGroupname).Returns("Relay_RG");
      A.CallTo(() => applicationSettingsService.RelayNameSpace).Returns(relayNameSpace);

      IRelayManagementApiClient relayManagementApiClient = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<HttpRequestException>(() => relayManagementApiClient.CreatePolicykeyAsync(TestHelper.TenantId, PolicyClaim.Listen));
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowExceptionWhenCreatePolicyKeyAzureSubscriptionIdIsNotPresentAsync()
    {
      // Arrange
      IApplicationSettingsService applicationSettingsService = A.Fake<IApplicationSettingsService>();
      var relayNameSpace = "Relay";
      A.CallTo(() => applicationSettingsService.AzureManagementApi).Returns(TestHelper.GetDefaultLocalHostUri.ToString());
      A.CallTo(() => applicationSettingsService.ResourceGroupname).Returns("Relay_RG");
      A.CallTo(() => applicationSettingsService.RelayNameSpace).Returns(relayNameSpace);

      IRelayManagementApiClient relayManagementApiClient = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => relayManagementApiClient.CreatePolicykeyAsync(TestHelper.TenantId, PolicyClaim.Listen));
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowExceptionWhenCreatePolicyKeyResourceGroupnameIsNotPresentAsync()
    {
      // Arrange
      IApplicationSettingsService applicationSettingsService = A.Fake<IApplicationSettingsService>();
      var relayNameSpace = "Relay";
      A.CallTo(() => applicationSettingsService.AzureManagementApi).Returns(TestHelper.GetDefaultLocalHostUri.ToString());
      A.CallTo(() => applicationSettingsService.AzureSubscriptionId).Returns("ffc1f3f6-a8ac-4d89-b485-02563edf182g");
      A.CallTo(() => applicationSettingsService.RelayNameSpace).Returns(relayNameSpace);

      IRelayManagementApiClient relayManagementApiClient = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => relayManagementApiClient.CreatePolicykeyAsync(TestHelper.TenantId, PolicyClaim.Listen));
    }
    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowExceptionWhenCreatePolicyKeyRelayNameSpaceIsNotPresentAsync()
    {
      // Arrange
      IApplicationSettingsService applicationSettingsService = A.Fake<IApplicationSettingsService>();
      A.CallTo(() => applicationSettingsService.AzureManagementApi).Returns(TestHelper.GetDefaultLocalHostUri.ToString());
      A.CallTo(() => applicationSettingsService.AzureSubscriptionId).Returns("ffc1f3f6-a8ac-4d89-b485-02563edf182g");
      A.CallTo(() => applicationSettingsService.ResourceGroupname).Returns("Relay_RG");

      IRelayManagementApiClient relayManagementApiClient = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => relayManagementApiClient.CreatePolicykeyAsync(TestHelper.TenantId, PolicyClaim.Listen));
    }
    [Fact]
    [Trait("Category", "UnitTest")]
    public async Task ThrowExceptionWhenCreatePolicyKeyTenantIdIsNotPresentAsync()
    {
      // Arrange
      IApplicationSettingsService applicationSettingsService = A.Fake<IApplicationSettingsService>();
      var relayNameSpace = "Relay";
      A.CallTo(() => applicationSettingsService.AzureManagementApi).Returns(TestHelper.GetDefaultLocalHostUri.ToString());
      A.CallTo(() => applicationSettingsService.AzureSubscriptionId).Returns("ffc1f3f6-a8ac-4d89-b485-02563edf182g");
      A.CallTo(() => applicationSettingsService.ResourceGroupname).Returns("Relay_RG");
      A.CallTo(() => applicationSettingsService.RelayNameSpace).Returns(relayNameSpace);

      IRelayManagementApiClient relayManagementApiClient = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => relayManagementApiClient.CreatePolicykeyAsync("", PolicyClaim.Listen));
    }
  }
}
