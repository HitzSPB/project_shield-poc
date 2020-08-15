namespace TeamTwo.CloudProvider.Management.Utilities
{
  public interface IApplicationSettingsService
  {
    string AadTenantId { get; }
    string AadClientId { get; }
    string AadClientSecret { get; }
    string AzureManagementApi { get; }
    string AzureSubscriptionId { get; }
    string ResourceGroupname { get; }
    string RelayNameSpace { get; }
  }
}