using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
namespace MyBlog.Model
{
  public class BlogNews:BaseId
  {
    //nvarchar带中文比较好
    [SugarColumn(ColumnDataType ="nvarchar(30)")]
    public string Title { get; set; }
    [SugarColumn(ColumnDataType ="text")]
    public string Content { get; set; }
    public DateTime Time { get; set; }
    public int BrowseCount { get; set; }
    public int LikeCount { get; set; }

    public int TypeId { get; set; }
    public int WriterId { get; set; }
    /// <summary>
    /// 类型，不映射到数据库
    /// </summary>
    [SugarColumn(IsIgnore =true)]
    public TypeInfo TypeInfo { get; set; }

    [SugarColumn(IsIgnore = true)]
    public WriterInfo WriterInfo { get; set; }
  }
}
