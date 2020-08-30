using System.Linq;
using TeamTwo.CloudShield.Shield.Infrastructures.Mappers;
using TeamTwo.CloudShield.Shield.Infrastructures.Models;
using TeamTwo.CloudShield.Shield.Service.Models;
using Xunit;

namespace TeamTwo.CloudShield.Shield.Test.Infrastructure.Mappers
{
  public class HybridConnectionDtoMapperMust
  {
    [Fact]
    public void ReturnHybridConnectionDtoWhenMapCallIsCalled()
    {
      // Arrange 
      var sut = new HybridConnectionDtoMapper();
      var hybridConnectionStorageDto = new HybridConnectionStorageDto(TestHelper.TenantId.ToString(), TestHelper.GetHybridConnectionDto());
      // Act 
      HybridConnectionDto actual = sut.HybridConnectionDtoMap(hybridConnectionStorageDto);
      // Assert
      Assert.Equal(hybridConnectionStorageDto.HybridConnection.HybridConnectionUrl, actual.HybridConnectionUrl);
      Assert.Equal(hybridConnectionStorageDto.HybridConnection.PolicyDtos.Count(), actual.PolicyDtos.Count());
    }

    [Fact]
    public void ReturnHybridConnectionStroageDtoWhenMapCallIsCalled()
    {
      // Arrange 
      var sut = new HybridConnectionDtoMapper();
      // Act 
      HybridConnectionStorageDto actual = sut.HybridConnectionStorageDtoMap(TestHelper.TenantId.ToString(), TestHelper.GetHybridConnectionDto());
      // Assert
      Assert.Equal(TestHelper.TenantId.ToString(), actual.Id);
      Assert.Equal(TestHelper.GetHybridConnectionDto().HybridConnectionUrl, actual.HybridConnection.HybridConnectionUrl);
      Assert.Equal(TestHelper.GetHybridConnectionDto().PolicyDtos.Count(), actual.HybridConnection.PolicyDtos.Count());
    }
  }
}
