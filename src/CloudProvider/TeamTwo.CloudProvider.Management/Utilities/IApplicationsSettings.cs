namespace TeamTwo.CloudProvider.Management.Utilities
{
  public interface IApplicationsSettings
  {
    string GetProcessEnvironmentVariable(string environmentName);
  }
}