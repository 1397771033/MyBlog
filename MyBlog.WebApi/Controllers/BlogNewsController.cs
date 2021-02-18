using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.WebApi.Utility.ApiResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class BlogNewsController : ControllerBase
  {
    private readonly IBlogNewsService _iBlogNewsService;
    public BlogNewsController(IBlogNewsService iBlogNewsService)
    {
      this._iBlogNewsService = iBlogNewsService;
    }
    [HttpGet("BlogNews")]
    public async Task<ActionResult<ApiResult>> GetBlogNews()
    {
      int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
      var data= await _iBlogNewsService.QueryAsync(c=>c.WriterId==id);
      if (data == null) return ApiResultHelper.Error("没有更多的文章");
      return ApiResultHelper.Success(data);
    }
    /// <summary>
    /// 添加文章
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    [HttpPost("Create")]
    public async Task<ActionResult<ApiResult>> Create(string title,string content,int typeid)
    {
      //数据验证
      BlogNews blogNews = new BlogNews
      {
        BrowseCount = 0,
        Content = content,
        LikeCount = 0,
        Time = DateTime.Now,
        Title = title,
        TypeId = typeid,
        WriterId = Convert.ToInt32(this.User.FindFirst("Id").Value)
      };
      bool b = await _iBlogNewsService.CreateAsync(blogNews);
      if (!b) return ApiResultHelper.Error("添加失败，服务器发生错误");
      return ApiResultHelper.Success(blogNews);
    }
    [HttpDelete("Delete")]
    public async Task<ActionResult<ApiResult>> Delete(int id)
    {
      bool b =await _iBlogNewsService.DeleteAsync(id);
      if (!b) return ApiResultHelper.Error("删除失败");
      return ApiResultHelper.Success(b);
    }
    [HttpPut("Edit")]
    public async Task<ActionResult<ApiResult>> Edit(int id,string title,string content,int typeid)
    {
      var blogNews= await _iBlogNewsService.FindAsync(id);
      if (blogNews == null) return ApiResultHelper.Error("没有找到该文章");
      blogNews.Title = title;
      blogNews.Content = content;
      blogNews.TypeId = typeid;
      bool b = await _iBlogNewsService.EditAsync(blogNews);
      if (!b) return ApiResultHelper.Error("修改失败");
      return ApiResultHelper.Success(blogNews);
    }
  }
}
