namespace TeamTwo.CloudShield.ShieldController.Utilities
{
  public interface IApplicationSettingsService
  {
    string GetProcessEnvironmentVariable(string environmentName);
  }
}
