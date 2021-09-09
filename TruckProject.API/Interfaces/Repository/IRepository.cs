using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TruckProject.API.Models;

namespace TruckProject.API.Interfaces.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(Guid id);
        IEnumerable<TEntity> GetAll();
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        void Dispose();
    }
}
