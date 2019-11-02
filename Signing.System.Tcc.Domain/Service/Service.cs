using Signing.System.Tcc.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Signing.System.Tcc.Domain.Service
{
    public class Service<TEntity> : IDisposable, IService<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> repository;

        public Service(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public void Dispose()
        {
            repository.Dispose();
        }

        public TEntity Get(int id)
        {
            return repository.Get(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return repository.GetAll();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return repository.Find(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return repository.FirstOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            repository.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            repository.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            repository.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            repository.RemoveRange(entities);
        }
    }
}
