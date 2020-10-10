using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Interfaces
{
    public interface IDataRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T t);
        Task<T> Get(int id);
        Task<T> Remove(int id);
        Task<T> Update(int id, T newT);
    }
}
