using Signing.System.Tcc.Application.Interfaces;
using Signing.System.Tcc.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Signing.System.Tcc.Application.Services
{
    public class AppService<TEntity> : IDisposable, IAppService<TEntity> where TEntity : class
    {
        private readonly IService<TEntity> service;

        public AppService(IService<TEntity> service)
        {
            this.service = service;
        }

        public TEntity Get(int id)
        {
            return service.Get(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return service.GetAll();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return service.Find(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return service.FirstOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            service.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            service.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            service.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            service.RemoveRange(entities);
        }

        public void Dispose()
        {
            service.Dispose();
        }
    }
}
