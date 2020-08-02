using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Utilities;
using TeamTwo.CloudProvider.Management.Services.Models;

namespace TeamTwo.CloudProvider.Management.Services
{
  public interface IRelayAzureManagement
  {
    Task<HybridConnectionDto> CreateHybridConnection(CreateRelayDto createRelayStorageDto);
  }
}
