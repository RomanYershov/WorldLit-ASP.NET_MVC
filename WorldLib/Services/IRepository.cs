using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldLib.Services
{
    interface IRepository<T> : IDisposable
    {
        List<T> Get();
        List<T> Get(Func<T, bool> predicate);
        void Create(params T[] models);
        void Update(T model);
        void Delete(T model);
        void Commit();
    }
}
