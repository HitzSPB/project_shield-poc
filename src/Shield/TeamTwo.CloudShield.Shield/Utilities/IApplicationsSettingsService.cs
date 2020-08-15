namespace TeamTwo.CloudShield.Shield.Utilities
{
  public interface IApplicationsSettingsService
  {
    // TEAMTWO-CLOUD_PROVIDER_URL
    string CloudProviderUrl { get; }
    string AccountEndPoint { get; }
    string Authkey { get; }
  }
}
