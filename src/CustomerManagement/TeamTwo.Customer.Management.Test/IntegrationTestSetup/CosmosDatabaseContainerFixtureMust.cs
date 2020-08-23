using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TeamTwo.Customer.Management.Test.IntegrationTestSetup
{
  [Collection(Const.CosmosDatabaseContainerCollection)]
  public class CosmosDatabaseContainerFixtureMust
  {
    private CosmosDatabaseContainerFixture DatabaseContainer { get; }

    public CosmosDatabaseContainerFixtureMust(CosmosDatabaseContainerFixture cosmosDatabaseContainer)
    {
      DatabaseContainer = cosmosDatabaseContainer;
    }

    [Fact]
    [Trait("Category","Integration")]
    [Trait("Category", "Container")]
    public void SetupValidDatabaseAccess()
    {
      Assert.Matches("^[a-fA-F0-9]+$", DatabaseContainer.ContainerId);
    }
    [Fact]
    [Trait("Category", "Integration")]
    [Trait("Category", "Container")]
    public void SetupValidDatabaseIp()
    {
      Assert.Matches(@"^(?:\d{1,3}\.?){4}$", DatabaseContainer.ContainerId);
    }
  }
}
