using TeamTwo.CloudShield.Shield.Infrastructures.Models;
using TeamTwo.CloudShield.Shield.Service.Model;

namespace TeamTwo.CloudShield.Shield.Infrastructures.Mappers
{
  public interface IHybridConnectionDtoMapper
  {
    HybridConnectionDto HybridConnectionDtoMap(HybridConnectionStorageDto hybridConnectionStorageDto);
    HybridConnectionStorageDto HybridConnectionStorageDtoMap(string id, HybridConnectionDto hybridConnectionStorageDto);
  }
}