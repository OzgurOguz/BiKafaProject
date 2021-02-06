using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiKafaProject.Core.Interfaces
{
    public interface ICrudRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync(string id);

        Task SaveAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task<DeleteResult> DeleteAsync(string id);


    }
}
