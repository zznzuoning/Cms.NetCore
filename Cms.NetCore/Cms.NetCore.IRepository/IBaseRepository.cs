using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cms.NetCore.IRepository
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {

        #region 同步
        /// <summary>
        /// 通过主键获取实体对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        T Get(Guid id);
        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetList();
        /// <summary>
        /// 执行具有条件的查询，并将结果映射到强类型列表
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns></returns>
        IEnumerable<T> GetList(ISpecification<T> whereConditions);
        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有Paging的强类型List
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">每页显示数据</param>
        /// <param name="whereConditions">查询条件</param>
        /// <param name="sortOrder">排序方式(倒序，正序)</param>
        /// <param name="sortPredicate">排序字段</param>
        /// <returns></returns>
        IEnumerable<T> GetListPaged(ISpecification<T> whereConditions, Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        /// <summary>
        /// 插入一条记录并返回主键值(自增类型返回主键值，否则返回null)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Insert(T entity);
        /// <summary>
        /// 更新一条数据并返回影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Update(T entity);
        /// <summary>
        /// 根据实体主键删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响的行数</returns>
        int Delete(Guid id);
        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回影响的行数</returns>
        int Delete(T entity);
        /// <summary>
        /// 条件删除多条记录
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns>影响的行数</returns>
        int DeleteList(ISpecification<T> whereConditions);
        /// <summary>
        /// 满足条件的记录数量
        /// </summary>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        int RecordCount(ISpecification<T> whereConditions);
        #endregion
        #region 异步
        /// <summary>
        /// 通过主键获取实体对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        Task<T> GetAsync(Guid id);
        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync();
        /// <summary>
        /// 执行具有条件的查询，并将结果映射到强类型列表
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync(ISpecification<T> whereConditions);
        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有Paging的强类型List
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">每页显示数据</param>
        /// <param name="whereConditions">查询条件</param>
        /// <param name="sortOrder">排序方式(倒序，正序)</param>
        /// <param name="sortPredicate">排序字段</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListPagedAsync(ISpecification<T> whereConditions, Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        /// <summary>
        /// 插入一条记录并返回主键值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> InsertAsync(T entity);
        /// <summary>
        /// 更新一条数据并返回影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);
        /// <summary>
        /// 根据实体主键删除一条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>影响的行数</returns>
        Task<int> DeleteAsync(Guid id);
        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回影响的行数</returns>
        Task<int> DeleteAsync(T entity);
        /// <summary>
        /// 条件删除多条记录
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns>影响的行数</returns>
        Task<int> DeleteListAsync(ISpecification<T> whereConditions);
        /// <summary>
        /// 满足条件的记录数量
        /// </summary>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        Task<int> RecordCountAsync(ISpecification<T> whereConditions);
        #endregion
    }
}
