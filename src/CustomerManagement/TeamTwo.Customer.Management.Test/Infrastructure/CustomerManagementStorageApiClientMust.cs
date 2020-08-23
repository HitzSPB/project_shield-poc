using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TeamTwo.Customer.Management.Apis;
using TeamTwo.Customer.Management.Apis.Models;
using TeamTwo.Customer.Management.Infrastructure;
using TeamTwo.Customer.Management.Services;
using TeamTwo.Customer.Management.Services.Models;
using Xunit;

namespace TeamTwo.Customer.Management.Test.Infrastructure
{
  public class CustomerManagementStorageApiClientMust
  {
    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task BeAbleToGetCustomerFromDatabaseAndReturnCorrectObjectAsync()
    {

    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task StoreCustomerAndReturnCorrectObjectAsync()
    {

    }
  }
}
