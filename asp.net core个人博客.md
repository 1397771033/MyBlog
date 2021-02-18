# 1.概述

## 为啥要做这个项目？

我发现刚接触.net的开发者入门的话比较困难，没有适中的项目拿来练手，而且我看B站上也没有几个asp.netcore的项目，所以我想着做一个比较简单，通俗易懂的个人博客项目，很简单的增删改查，让刚接触.net的开发者更好学习asp.net core，这个项目我准备用asp.net core webapi+elementui来做，虽然这个项目很简单，但是麻雀虽小但五脏俱全，我还会用一些比较常用的架构来设计这个项目。每期视频我都会尽量按照10-20分钟左右时间来录制，可以更好的接受新知识

# 2.数据库设计

文章表

```sql
ID
文章标题
文章内容
创建时间
文章类型ID
浏览量
点赞量
作者ID
```

文章类型表

```sql
ID
类型名
```

作者表

```sql
ID
姓名
账号
密码 MD5
```

# 3.架构设计

仓储层

服务层

# MD5加密

```C#
public static string MD5Encrypt32(string password)
{
    string pwd = "";
    MD5 md5 = MD5.Create(); //实例化一个md5对像
    byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
    for (int i = 0; i < s.Length; i++)
    {
        pwd = pwd + s[i].ToString("X");
    }
    return pwd;
}
```

# JWT使用

## JWT授权

1.添加一个webapi项目

2.安装Nuget程序包 System.IdentityModel.Tokens.Jwt 

```C#
var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, "张三")
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
return jwtToken;
```

## JWT鉴权

安装Microsoft.AspNetCore.Authentication.JwtBearer

```C#
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF")),
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:6060",
                ValidateAudience = true,
                ValidAudience = "http://localhost:5000",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(60)
              };
            });
```

## JWT授权鉴权使用

Swagger想要使用鉴权需要注册服务的时候添加以下代码

```C#
c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          In=ParameterLocation.Header,
          Type=SecuritySchemeType.ApiKey,
          Description= "直接在下框中输入Bearer {token}（注意两者之间是一个空格）",
          Name="Authorization",
          BearerFormat="JWT",
          Scheme="Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
              Reference=new OpenApiReference
              {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
              }
            },
            new string[] {}
          }
        });
```

# AutoMapper

安装Nuget AutoMapper.Extensions.Microsoft.DependencyInjection

定义一个类，继承Profile

```C#
public class CustomAutoMapperProfile:Profile
  {
    public CustomAutoMapperProfile()
    {
      base.CreateMap<StudentEntity, StudentDto>();
    }
  }
```

在服务中注册

```C#
services.AddAutoMapper(typeof(CustomAutoMapperProfile));
```

构造函数注入

```C#
private readonly IMapper _mapper;

    public StudentsController(IMapper mapper)
    {
      this._mapper = mapper;
    }
```

复杂映射

```C#
base.CreateMap<Admin, AdminDto>()
        .ForMember(dest => dest.RoleMsg, sourse => sourse.MapFrom(src => src.RoleInfo.RoleMsg));
```

```C#
User:
UserPwd	->不能返回到前端
UserName ->返回到前端
UserDTO:
UserName

```

