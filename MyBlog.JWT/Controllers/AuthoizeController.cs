using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBlog.IService;
using MyBlog.JWT.Utility._MD5;
using MyBlog.JWT.Utility.ApiResult;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.JWT.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthoizeController : ControllerBase
  {
    private readonly IWriterInfoService _iWriterInfoService;
    public AuthoizeController(IWriterInfoService iWriterInfoService)
    {
      _iWriterInfoService = iWriterInfoService;
    }
    [HttpPost("Login")]
    public async Task<ApiResult> Login(string username,string userpwd)
    {
      //加密后的密码 123456 =>sdlkfjkldsjidaifdaskfaj == sdlkfjkldsjidaifdaskfaj
      string pwd = MD5Helper.MD5Encrypt32(userpwd);
      //数据校验
      var writer= await  _iWriterInfoService.FindAsync(c => c.UserName == username && c.UserPwd == pwd);
      if (writer != null)
      {
        //登陆成功
        var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, writer.Name),
                new Claim("Id", writer.Id.ToString()),
                new Claim("UserName", writer.UserName)
                //不能放敏感信息 
            };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF"));
        //issuer代表颁发Token的Web应用程序，audience是Token的受理者
        var token = new JwtSecurityToken(
            issuer: "http://localhost:6060",
            audience: "http://localhost:5000",
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        return ApiResultHelper.Success(jwtToken);
      }
      else
      {
        return ApiResultHelper.Error("账号或密码错误");
      }
    }
  }
}
