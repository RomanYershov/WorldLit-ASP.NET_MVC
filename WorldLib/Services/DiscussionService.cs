using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WorldLib.Models;

namespace WorldLib.Services
{
    public class DiscussionService : IService<Discussion>
    {
        private ApplicationDbContext db;

        public DiscussionService()
        {
            db = new ApplicationDbContext();
        }

        public void Commit()
        {
            db.SaveChanges();
        }

        public void Create(Discussion entity)
        {
            db.Discussions.Add(entity);
        }

        public void Delete(Discussion entity)
        {
            db.Discussions.Remove(entity);
        }

        public IEnumerable<Discussion> Get()
        {
            return db.Discussions;
        }

        public Discussion GetById(int id)
        {
            return db.Discussions.Find(id);
        }

        public void Update(Discussion entity)
        {
            db.Entry(entity).State = EntityState.Modified;
           
        }
    }
   
}