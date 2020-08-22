﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamTwo.CloudShield.DemoApiServer.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ChatController : ControllerBase
  {
    // GET: api/<ChatController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<ChatController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/<ChatController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
      throw new NotImplementedException();
    }

    // PUT api/<ChatController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
      throw new NotImplementedException();
    }

    // DELETE api/<ChatController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
      throw new NotImplementedException();
    }
  }
}
