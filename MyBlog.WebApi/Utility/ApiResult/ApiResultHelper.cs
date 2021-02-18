using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Utility.ApiResult
{
  public static class ApiResultHelper
  {
    //成功后返回的数据
    public static ApiResult Success(dynamic data)
    {
      return new ApiResult
      {
        Code = 200,
        Data = data,
        Msg = "操作成功",
        Total = 0
      };
    }
    public static ApiResult Success(dynamic data, RefAsync<int> total)
    {
      return new ApiResult
      {
        Code = 200,
        Data = data,
        Msg = "操作成功",
        Total = total
      };
    }
    public static ApiResult Error(string msg)
    {
      return new ApiResult
      {
        Code = 500,
        Data = null,
        Msg = msg,
        Total = 0
      };
    }
  }
}
