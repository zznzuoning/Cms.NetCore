using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Infrastructure.Specifications;
using Cms.NetCore.IRepository;
using Cms.NetCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.NetCore.Repository
{
    public class EntityFrameworkRepository<T> : IBaseRepository<T> where T : class, IEntity, new()
    {
        public EntityFrameworkRepository(IEntityFrameworkRepositoryContext Context)
        {
            this.EFContext = Context;
        }

        public int Delete(T entity)
        {
            EFContext.Context.Set<T>().Remove(entity);
            return EFContext.Context.SaveChanges();
        }
        public async Task<int> DeleteAsync(T entity)
        {
            EFContext.Context.Set<T>().Remove(entity);
            return await EFContext.Context.SaveChangesAsync();
        }

        public int DeleteList(ISpecification<T> whereConditions)
        {
            var tList = EFContext.Context.Set<T>().Where(whereConditions.GetExpression());
            EFContext.Context.Set<T>().RemoveRange(tList);
            return EFContext.Context.SaveChanges();
        }

        public async Task<int> DeleteListAsync(ISpecification<T> whereConditions)
        {
            var tList = EFContext.Context.Set<T>().Where(whereConditions.GetExpression());
            EFContext.Context.Set<T>().RemoveRange(tList);
            return await EFContext.Context.SaveChangesAsync();
        }

        public T Get(Guid id) => EFContext.Context.Set<T>().Find(id);

        public async Task<T> GetAsync(Guid id) => await EFContext.Context.Set<T>().FindAsync(id);

        public List<T> GetList() => EFContext.Context.Set<T>().ToList();

        public List<T> GetList(ISpecification<T> whereConditions) => EFContext.Context.Set<T>().Where(whereConditions.GetExpression()).ToList();

        public async Task<List<T>> GetListAsync() => await EFContext.Context.Set<T>().ToListAsync();

        public async Task<List<T>> GetListAsync(ISpecification<T> whereConditions) => await EFContext.Context.Set<T>().Where(whereConditions.GetExpression()).ToListAsync();

        public List<T> GetListPaged(ISpecification<T> whereConditions, System.Linq.Expressions.Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "页码必须大于或等于1。");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "每页大小必须大于或等于1。");
            var query = EFContext.Context.Set<T>().Where(whereConditions.GetExpression());
            int skip = ( pageNumber - 1 ) * pageSize;
            int take = pageSize;
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        var pagedGroupAscending = query.OrderBy(sortPredicate).Skip(skip).Take(take).ToList();
                        return pagedGroupAscending;
                    case SortOrder.Descending:
                        var pagedGroupDescending = query.OrderByDescending(sortPredicate).Skip(skip).Take(take).ToList();
                        return pagedGroupDescending;
                    default:
                        break;
                }
            }
            throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");
        }

        public async Task<List<T>> GetListPagedAsync(ISpecification<T> whereConditions, System.Linq.Expressions.Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "页码必须大于或等于1。");
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "每页大小必须大于或等于1。");
            var query = EFContext.Context.Set<T>().Where(whereConditions.GetExpression());
            int skip = ( pageNumber - 1 ) * pageSize;
            int take = pageSize;
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        var pagedGroupAscending = await query.OrderBy(sortPredicate).Skip(skip).Take(take).ToListAsync();
                        return pagedGroupAscending;
                    case SortOrder.Descending:
                        var pagedGroupDescending = await query.OrderByDescending(sortPredicate).Skip(skip).Take(take).ToListAsync();
                        return pagedGroupDescending;
                    default:
                        break;
                }
            }
            throw new InvalidOperationException("基于分页功能的查询必须指定排序字段和排序顺序。");
        }

        public int Insert(T entity)
        {
            EFContext.Context.Set<T>().Add(entity);
            return EFContext.Context.SaveChanges();
        }

        public async Task<int> InsertAsync(T entity)
        {
            EFContext.Context.Set<T>().Add(entity);
            return await EFContext.Context.SaveChangesAsync();
        }

        public int RecordCount(ISpecification<T> whereConditions) => EFContext.Context.Set<T>().Count(whereConditions.GetExpression());
        

        public async Task<int> RecordCountAsync(ISpecification<T> whereConditions)=>await EFContext.Context.Set<T>().CountAsync(whereConditions.GetExpression());


        public int Update(T entity)
        {
            EFContext.Context.Set<T>().Update(entity);
            return EFContext.Context.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            EFContext.Context.Set<T>().Update(entity);
            return await EFContext.Context.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        //对外调用的上下文
        public IEntityFrameworkRepositoryContext EFContext { get; }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~EntityFrameworkRepository() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
