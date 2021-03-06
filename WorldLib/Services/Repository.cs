﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WorldLib.Models;

namespace WorldLib.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public Repository()
        {
            _context = new ApplicationDbContext();
            _dbSet = _context.Set<T>();
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Create(params T[] models)
        {
            _dbSet.AddRange(models);
        }

        public void Delete(T model)
        {
            _dbSet.Remove(model);
        }

        public void Dispose()
        {
            Dispose();
        }

        public List<T> Get()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public List<T> Get(Func<T, bool> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public void Update(T model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }
    }
}