using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
namespace MyBlog.Model
{
  public class TypeInfo:BaseId
  {
    [SugarColumn(ColumnDataType ="nvarchar(12)")]
    public string Name { get; set; }
  }
}
