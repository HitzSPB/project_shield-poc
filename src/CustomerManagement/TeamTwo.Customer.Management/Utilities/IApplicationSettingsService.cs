namespace TeamTwo.Customer.Management.Utilities
{
  public interface IApplicationSettingsService
  {
    string GetProccessEnvironmentVariable(string environmentVariableName);
  }
}