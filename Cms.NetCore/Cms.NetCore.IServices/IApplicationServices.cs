using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Infrastructure.Specifications;
using Cms.NetCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cms.NetCore.IServices
{
    public interface IApplicationServices<T> : IDisposable where T : class
    {
        #region 同步
        /// <summary>
        /// 通过主键获取实体对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        DataResult<T> Get(Guid id);
        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        ListResult<T> GetList();
        /// <summary>
        /// 执行具有条件的查询，并将结果映射到强类型列表
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns></returns>
        ListResult<T> GetList(ISpecification<T> whereConditions);
        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有Paging的强类型List
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">每页显示数据</param>
        /// <param name="whereConditions">查询条件</param>
        /// <param name="sortOrder">排序方式(倒序，正序)</param>
        /// <param name="sortPredicate">排序字段</param>
        /// <returns></returns>
        PageResult<T> GetListPaged(ISpecification<T> whereConditions, Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        /// <summary>
        /// 插入一条记录并返回主键值(自增类型返回主键值，否则返回null)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Result Insert(T entity);
        /// <summary>
        /// 更新一条数据并返回影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Result Update(T entity);

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回影响的行数</returns>
        Result Delete(T entity);
        /// <summary>
        /// 条件删除多条记录
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns>影响的行数</returns>
        Result DeleteList(ISpecification<T> whereConditions);
        /// <summary>
        /// 满足条件的记录数量
        /// </summary>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        CountResult RecordCount(ISpecification<T> whereConditions);
        #endregion
        #region 异步
        /// <summary>
        /// 通过主键获取实体对象
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        Task<DataResult<T>> GetAsync(Guid id);
        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        Task<ListResult<T>> GetListAsync();
        /// <summary>
        /// 执行具有条件的查询，并将结果映射到强类型列表
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns></returns>
        Task<ListResult<T>> GetListAsync(ISpecification<T> whereConditions);
        /// <summary>
        /// 使用where子句执行查询，并将结果映射到具有Paging的强类型List
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">每页显示数据</param>
        /// <param name="whereConditions">查询条件</param>
        /// <param name="sortOrder">排序方式(倒序，正序)</param>
        /// <param name="sortPredicate">排序字段</param>
        /// <returns></returns>
        Task<PageResult<T>> GetListPagedAsync(ISpecification<T> whereConditions, Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        /// <summary>
        /// 插入一条记录并返回主键值
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Result> InsertAsync(T entity);
        /// <summary>
        /// 更新一条数据并返回影响的行数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Result> UpdateAsync(T entity);

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>返回影响的行数</returns>
        Task<Result> DeleteAsync(T entity);
        /// <summary>
        /// 条件删除多条记录
        /// </summary>
        /// <param name="whereConditions">条件</param>
        /// <returns>影响的行数</returns>
        Task<Result> DeleteListAsync(ISpecification<T> whereConditions);
        /// <summary>
        /// 满足条件的记录数量
        /// </summary>
        /// <param name="whereConditions"></param>
        /// <returns></returns>
        Task<CountResult> RecordCountAsync(ISpecification<T> whereConditions);
        #endregion
    }
}
