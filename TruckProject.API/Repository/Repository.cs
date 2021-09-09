using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TruckProject.API.Context;
using TruckProject.API.Interfaces.Repository;
using TruckProject.API.Models;

namespace TruckProject.API.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MyContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(MyContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate).ToList();
        }

        public virtual TEntity GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual bool Add(TEntity entity)
        {
            DbSet.Add(entity);
            return SaveChanges();
        }

        public virtual bool Update(TEntity entity)
        {
            DbSet.Update(entity);
            return SaveChanges();
        }

        public virtual bool Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            return SaveChanges();
        }

        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
