using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldLib.Services
{
    interface IRepository<T> : IDisposable
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Func<T, bool> predicate);
        void Create(T models);
        void Update(T model);
        void Delete(T model);
        void Commit();
    }
}
