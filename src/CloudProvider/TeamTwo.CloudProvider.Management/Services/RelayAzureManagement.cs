﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamTwo.CloudProvider.Management.Services.Models;

namespace TeamTwo.CloudProvider.Management.Services
{
  public class RelayAzureManagement : IRelayAzureManagement
  {
    Task<HybridConnectionDto> IRelayAzureManagement.CreateHybridConnection(CreateRelayDto createRelayStorageDto)
    {
      throw new NotImplementedException();
    }
  }
}
