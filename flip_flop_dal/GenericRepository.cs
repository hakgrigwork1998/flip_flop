using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace flip_flop_dal
{
    public class GenericRepository<EntityType> : IRepository<EntityType>, IDisposable
        where EntityType : class
    {
        protected readonly FlipFlopContext _ctx;
        public GenericRepository(FlipFlopContext context)
        {
            _ctx = context;
        }

        public void Update(EntityType entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public void Add(IEnumerable<EntityType> entitylist)
        {
            _ctx.Set<EntityType>().AddRange(entitylist);
        }

        public void Add(EntityType entity)
        {
            _ctx.Set<EntityType>().Add(entity);
        }

        public void Delete(IEnumerable<EntityType> entitylist)
        {
            _ctx.Set<EntityType>().RemoveRange(entitylist);
        }

        public void Delete(EntityType entity)
        {
            _ctx.Set<EntityType>().Remove(entity);
        }

        public IEnumerable<EntityType> FetchAll()
        {
            return _ctx.Set<EntityType>().ToList();
        }

        public EntityType FetchByPrimaryKey(object primarykey)
        {
            return _ctx.Set<EntityType>().Find(primarykey);
        }

        public IEnumerable<EntityType> FindWhere(Expression<Func<EntityType, bool>> predicate)
        {
            return _ctx.Set<EntityType>().Where(predicate).ToList();
        }

        public EntityType FirstOrDefault(Expression<Func<EntityType, bool>> predicate)
        {
            return _ctx.Set<EntityType>().Where(predicate).FirstOrDefault();
        }

        public virtual void Save()
        {
            _ctx.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
