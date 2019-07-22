using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Infrastructure.Extension;
using Cms.NetCore.Infrastructure.Specifications;
using Cms.NetCore.IRepository;
using Cms.NetCore.IServices;
using Cms.NetCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cms.NetCore.Services
{
    public class ApplicationServices<T> : IApplicationServices<T> where T : class, new()
    {
        private readonly IBaseRepository<T> _baseRepository;
        public ApplicationServices(IBaseRepository<T> baseRepository)
        {
            this._baseRepository = baseRepository;
        }

        public Result Delete(T entity)
        {
            var result = new Result();

            if (entity == null)
            {
                result.code = (int)StatusCodeEnum.ParameterError;
                result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                return result;
            }

            int isDelete = _baseRepository.Delete(entity);
            if (isDelete == 0)
            {
                result.code = (int)StatusCodeEnum.Accepted;
                result.msg = StatusCodeEnum.Accepted.GetEnumText();
            }
            return result;
        }

        public async Task<Result> DeleteAsync(T entity)
        {
            var result = new Result();

            if (entity == null)
            {
                result.code = (int)StatusCodeEnum.ParameterError;
                result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                return result;
            }

            int isDelete = await _baseRepository.DeleteAsync(entity);
            if (isDelete == 0)
            {
                result.code = (int)StatusCodeEnum.Accepted;
                result.msg = StatusCodeEnum.Accepted.GetEnumText();
            }
            return result;

        }

        public Result DeleteList(ISpecification<T> whereConditions)
        {
            var result = new Result();

            int isDelete = _baseRepository.DeleteList(whereConditions);
            if (isDelete == 0)
            {
                result.code = (int)StatusCodeEnum.Accepted;
                result.msg = StatusCodeEnum.Accepted.GetEnumText();
            }
            return result;
        }

        public async Task<Result> DeleteListAsync(ISpecification<T> whereConditions)
        {
            var result = new Result();

            int isDelete = await _baseRepository.DeleteListAsync(whereConditions);
            if (isDelete == 0)
            {
                result.code = (int)StatusCodeEnum.Accepted;
                result.msg = StatusCodeEnum.Accepted.GetEnumText();
            }
            return result;


        }



        public DataResult<T> Get(Guid id)
        {
            var result = new DataResult<T>();

            if (id == Guid.Empty || id == null)
            {
                result.code = (int)StatusCodeEnum.ParameterError;
                result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                return result;
            }
            T t = _baseRepository.Get(id);
            if (t != null)
            {
                result.data = t;
            }
            return result;


        }

        public async Task<DataResult<T>> GetAsync(Guid id)
        {
            var result = new DataResult<T>();

            if (id == Guid.Empty || id == null)
            {
                result.code = (int)StatusCodeEnum.ParameterError;
                result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                return result;
            }
            T t = new T();
            t = await _baseRepository.GetAsync(id);
            if (t != null)
            {
                result.data = t;
            }
            return result;


        }

        public ListResult<T> GetList()
        {
            var result = new ListResult<T>();

            List<T> t = _baseRepository.GetList();
            if (t != null)
            {
                result.data = t;
            }
            return result;


        }

        public ListResult<T> GetList(ISpecification<T> whereConditions)
        {
            var result = new ListResult<T>();

            List<T> t = _baseRepository.GetList(whereConditions);
            if (t != null)
            {
                result.data = t;
            }
            return result;


        }

        public async Task<ListResult<T>> GetListAsync()
        {
            var result = new ListResult<T>();

            List<T> t = await _baseRepository.GetListAsync();
            if (t != null)
            {
                result.data = t;
            }
            return result;


        }

        public async Task<ListResult<T>> GetListAsync(ISpecification<T> whereConditions)
        {
            var result = new ListResult<T>();

            List<T> t = await _baseRepository.GetListAsync(whereConditions);
            if (t != null)
            {
                result.data = t;
            }
            return result;


        }

        public PageResult<T> GetListPaged(ISpecification<T> whereConditions, Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            var result = new PageResult<T>();

            List<T> t = _baseRepository.GetListPaged(whereConditions, sortPredicate, sortOrder, pageNumber, pageSize);
            if (t != null)
            {
                result.count = t.Count;
                result.data = t;
            }
            return result;


        }

        public async Task<PageResult<T>> GetListPagedAsync(ISpecification<T> whereConditions, Expression<Func<T, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            var result = new PageResult<T>();
            List<T> t = await _baseRepository.GetListPagedAsync(whereConditions, sortPredicate, sortOrder, pageNumber, pageSize);
            if (t != null)
            {
                result.count = t.Count;
                result.data = t;
            }
            return result;

        }

        public Result Insert(T entity)
        {
            var result = new Result();

            if (entity == null)
            {
                result.code = (int)StatusCodeEnum.ParameterError;
                result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                return result;
            }
            int isInsert = _baseRepository.Insert(entity);
            if (isInsert == 0)
            {
                result.code = (int)StatusCodeEnum.Accepted;
                result.msg = StatusCodeEnum.Accepted.GetEnumText();
                return result;
            }
            return result;


        }

        public async Task<Result> InsertAsync(T entity)
        {
            var result = new Result();

            if (entity == null)
            {
                result.code = (int)StatusCodeEnum.ParameterError;
                result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                return result;
            }
            int isInsert = await _baseRepository.InsertAsync(entity);
            if (isInsert == 0)
            {
                result.code = (int)StatusCodeEnum.Accepted;
                result.msg = StatusCodeEnum.Accepted.GetEnumText();
                return result;
            }
            return result;


        }

        public CountResult RecordCount(ISpecification<T> whereConditions)
        {
            var result = new CountResult();

            result.ResultCount = _baseRepository.RecordCount(whereConditions);
            return result;


        }

        public async Task<CountResult> RecordCountAsync(Infrastructure.Specifications.ISpecification<T> whereConditions)
        {
            var result = new CountResult();

            result.ResultCount = await _baseRepository.RecordCountAsync(whereConditions);
            return result;



        }

        public Result Update(T entity)
        {
            var result = new Result();

            if (entity == null)
            {
                result.code = (int)StatusCodeEnum.ParameterError;
                result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                return result;
            }
            int isUpdate = _baseRepository.Update(entity);
            if (isUpdate == 0)
            {
                result.code = (int)StatusCodeEnum.Accepted;
                result.msg = StatusCodeEnum.Accepted.GetEnumText();
                return result;
            }
            return result;


        }

        public async Task<Result> UpdateAsync(T entity)
        {
            var result = new Result();

            if (entity == null)
            {
                result.code = (int)StatusCodeEnum.ParameterError;
                result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                return result;
            }
            int isUpdate = await _baseRepository.UpdateAsync(entity);
            if (isUpdate == 0)
            {
                result.code = (int)StatusCodeEnum.Accepted;
                result.msg = StatusCodeEnum.Accepted.GetEnumText();
                return result;
            }
            return result;


        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

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
        // ~ApplicationServices() {
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
