using Xunit;

namespace TeamTwo.Customer.Management.Test.IntegrationTestSetup
{
  [CollectionDefinition(Const.CosmosDatabaseContainerCollection)]
  public class CosmosDatabaseContainerCollection : ICollectionFixture<CosmosDatabaseContainerFixture>
  {
    // This class has no code, and is never created. 
    // It's purpose is to be the place to apply [CollectionDefinition] and the ICollectionFixture Interface
  }
}
