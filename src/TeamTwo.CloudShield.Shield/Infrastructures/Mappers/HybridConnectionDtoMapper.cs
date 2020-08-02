using System;
using System.Collections.Generic;
using System.Text;
using TeamTwo.CloudShield.Shield.Infrastructures.Models;
using TeamTwo.CloudShield.Shield.Service.Model;

namespace TeamTwo.CloudShield.Shield.Infrastructures.Mappers
{
  public class HybridConnectionDtoMapper : IHybridConnectionDtoMapper
  {
    public HybridConnectionDto HybridConnectionDtoMap(HybridConnectionStorageDto hybridConnectionStorageDto)
    {
      return new HybridConnectionDto(hybridConnectionStorageDto.HybridConnection.HybridConnectionUrl, hybridConnectionStorageDto.HybridConnection.PolicyDtos);
    }
    public HybridConnectionStorageDto HybridConnectionStorageDtoMap(string id, HybridConnectionDto hybridConnectionStorageDto)
    {
      return new HybridConnectionStorageDto(id, new HybridConnectionDto(hybridConnectionStorageDto.HybridConnectionUrl, hybridConnectionStorageDto.PolicyDtos));
    }
  }
}
