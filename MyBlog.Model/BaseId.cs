using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
namespace MyBlog.Model
{
  public class BaseId
  {
    [SugarColumn(IsIdentity=true, IsPrimaryKey=true)]
    public int Id { get; set; }
  }
}
