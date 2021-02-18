using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Repository;
using MyBlog.Service;
using MyBlog.WebApi.Utility._AutoMapper;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyBlog.WebApi", Version = "v1" });

        #region Swaggerʹ�ü�Ȩ���
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Description = "ֱ�����¿�������Bearer {token}��ע������֮����һ���ո�",
          Name = "Authorization",
          BearerFormat = "JWT",
          Scheme = "Bearer"
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
        #endregion
      });
      #region SqlSugarIOC
      services.AddSqlSugar(new IocConfig()
      {
        ConnectionString = this.Configuration["SqlConn"],
        DbType = IocDbType.SqlServer,
        IsAutoCloseConnection = true
      });
      #endregion
      #region IOC����ע��
      services.AddCustomIOC();
      #endregion
      #region JWT��Ȩ
      services.AddCustomJWT();
      #endregion
      #region AutoMapper
      services.AddAutoMapper(typeof(CustomAutoMapperProfile));
      #endregion
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyBlog.WebApi v1"));
      }
      app.UseRouting();
      //��ӵ��ܵ��� ��Ȩ
      app.UseAuthentication();
      //��Ȩ
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
  public static class IOCExtend
  {
    public static IServiceCollection AddCustomIOC(this IServiceCollection services)
    {
      services.AddScoped<IBlogNewsRepository, BlogNewsRepository>();
      services.AddScoped<IBlogNewsService, BlogNewsService>();
      services.AddScoped<ITypeInfoRepository, TypeInfoRepository>();
      services.AddScoped<ITypeInfoService, TypeInfoService>();
      services.AddScoped<IWriterInfoRepository, WriterInfoRepository>();
      services.AddScoped<IWriterInfoService, WriterInfoService>();
      return services;
    }
    public static IServiceCollection AddCustomJWT(this IServiceCollection services)
    {
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
      return services;
    }
  }
}
