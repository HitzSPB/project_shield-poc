﻿using System;

namespace TeamTwo.CloudShield.Shield.Service.Model
{
  public class HybridConnectionDto
  {
    public HybridConnectionDto(Uri hybridConnectionUrl, PolicyDto[] policyDtos)
    {
      HybridConnectionUrl = hybridConnectionUrl;
      PolicyDtos = policyDtos;
    }

    public Uri HybridConnectionUrl { get; set; }
    public PolicyDto[] PolicyDtos { get; }
  }
}
