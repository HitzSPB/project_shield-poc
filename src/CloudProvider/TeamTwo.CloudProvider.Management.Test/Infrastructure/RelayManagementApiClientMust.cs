using System;
using System.Net.Http;
using System.Threading.Tasks;
using FakeItEasy;
using TeamTwo.CloudProvider.Management.Infrastructure;
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

      IRelayManagementApiClient sut = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act
      Uri actual = await sut.CreateHybridConnectionAsync(TestHelper.TenantId);

      // Assert
      Assert.Contains(TestHelper.TenantId, actual.OriginalString);
      Assert.Contains(relayNameSpace, actual.OriginalString);
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

      IRelayManagementApiClient sut = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<HttpRequestException>(() => sut.CreateHybridConnectionAsync(TestHelper.TenantId));
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
        IRelayManagementApiClient sut = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));
      }
      catch (UriFormatException exception)
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
        IRelayManagementApiClient sut = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));
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

      IRelayManagementApiClient sut = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => sut.CreateHybridConnectionAsync(TestHelper.TenantId));
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

      IRelayManagementApiClient sut = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => sut.CreateHybridConnectionAsync(TestHelper.TenantId));
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

      IRelayManagementApiClient sut = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => sut.CreateHybridConnectionAsync(TestHelper.TenantId));
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

      IRelayManagementApiClient sut = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => sut.CreateHybridConnectionAsync(""));
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

      IRelayManagementApiClient sut = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act
      PolicyDto actual = await sut.CreatePolicykeyAsync(TestHelper.TenantId, PolicyClaim.Listen);

      // Assert
      Assert.NotNull(actual);
      Assert.Equal(TestHelper.PrimaryKey, actual.PolicyKey);
      Assert.True(Guid.TryParse(actual.PolicyName, out Guid policyNameAsGuid));
      Assert.Equal(PolicyClaim.Listen, actual.PolicyType);
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

      IRelayManagementApiClient sut = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<HttpRequestException>(() => sut.CreatePolicykeyAsync(TestHelper.TenantId, PolicyClaim.Listen));
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

      IRelayManagementApiClient sut = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => sut.CreatePolicykeyAsync(TestHelper.TenantId, PolicyClaim.Listen));
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

      IRelayManagementApiClient sut = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => sut.CreatePolicykeyAsync(TestHelper.TenantId, PolicyClaim.Listen));
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

      IRelayManagementApiClient sut = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<InvalidOperationException>(() => sut.CreatePolicykeyAsync(TestHelper.TenantId, PolicyClaim.Listen));
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

      IRelayManagementApiClient sut = new RelayManagementApiClient(applicationSettingsService, new HttpClient(new RelayManagementClientHttpMock()));

      // Act & Assert
      await Assert.ThrowsAsync<ArgumentNullException>(() => sut.CreatePolicykeyAsync("", PolicyClaim.Listen));
    }
  }
}
