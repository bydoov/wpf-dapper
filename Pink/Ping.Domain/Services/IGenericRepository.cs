using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ping.Domain.Services
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task<int> Add(T entity);

        Task<int> Update(int id, T entity);

        Task<int> Delete(int id);
    }
}
