using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TestController : ControllerBase
  {
    [HttpGet("NoAuthorize")]
    public string NoAuthorize()
    {
      return "this is NoAuthorize";
    }
    [Authorize]
    [HttpGet("Authorize")]
    public string Authorize()
    {
      return "this is Authorize";
    }
  }
}
