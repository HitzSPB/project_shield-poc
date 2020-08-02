using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Utilities;
using TeamTwo.CloudShield.Shield.Service.Models;
using TeamTwo.CloudShield.Shield.Services.Models;

namespace TeamTwo.CloudShield.Shield.Services
{
  public interface IRelayManagementService
  {
    Task<HybridConnectionDto> GetRelayAsync(string tenantId);
    Task<HybridConnectionDto> StoreRelayAsync(CreateRelayStorageDto createRelayStorageDto);

  }
}
