namespace TeamTwo.CloudProvider.Management.Infrastructure.Models
{
  public class GetPolicyKeysResponse
  {
    public string PrimaryConnectionString { get; set; }
    public string PrimaryKey { get; set; }
    public string KeyName { get; set; }
  }
}
