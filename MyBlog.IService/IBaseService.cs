using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IService
{
  public interface IBaseService<TEntity> where TEntity:class,new()
  {
    Task<bool> CreateAsync(TEntity entity);
    Task<bool> DeleteAsync(int id);
    Task<bool> EditAsync(TEntity entity);
    Task<TEntity> FindAsync(int id);
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func);
    /// <summary>
    /// 查询全部的数据
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> QueryAsync();
    /// <summary>
    /// 自定义条件查询
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func);
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <param name="total"></param>
    /// <returns></returns>
    Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total);
    /// <summary>
    /// 自定义条件分页查询
    /// </summary>
    /// <param name="func"></param>
    /// <param name="page"></param>
    /// <param name="size"></param>
    /// <param name="total"></param>
    /// <returns></returns>
    Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total);
  }
}
