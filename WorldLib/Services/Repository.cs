using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WorldLib.Models;

namespace WorldLib.Services
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class
    {
        private DbSet<T> _dbSet;
        public Repository()
        {
            _context = new ApplicationDbContext();
            _dbSet = _context.Set<T>();
        }
        public Repository(RepositoryBase repository)
        {
            _context = repository._context;
            _dbSet = _context.Set<T>();
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Create(T models)
        {
            _dbSet.Attach(models);
            _dbSet.Add(models);
        }

        public void Delete(T model)
        {
            _context.Entry(model).State = EntityState.Deleted;
            _dbSet.Remove(model);
        }

        public void Delete(Func<T, bool> predicate)
        {
             var model = _dbSet.AsNoTracking().Where(predicate).SingleOrDefault();
            if (model == null) return;
            _context.Entry(model).State = EntityState.Deleted;
            _dbSet.Remove(model);
        }


        public IEnumerable<T> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public void Update(T model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }

        //public void Update(Func<T, bool> predicate)
        //{
        //     var model = _dbSet.AsNoTracking().Where(predicate).SingleOrDefault();
        //    _context.Entry(model).State = EntityState.Modified;
        //}
        public IEnumerable<T> GetWithInclude(Func<T, bool> predicate,
    params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
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

    public class RepositoryBase
    {
        public ApplicationDbContext _context;
    }
}