namespace TeamTwo.CloudProvider.Management.Utilities
{
  public interface IApplicationSettingsService
  {
    string GetProcessEnvironmentVariable(string environmentName);
  }
}