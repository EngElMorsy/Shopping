using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMDomain.Abstraction
{
    public interface IGenericRepostiry<TEntity>where TEntity:BaseEntity
    {
        IQueryable<TEntity> GetAll(); 
        Task<TEntity?> GetIdAsync(Guid id,CancellationToken cancellationToken=default);
        Task<TEntity> CreateAsync(TEntity entity,CancellationToken cancellationToken=default);
        Task CreateRangeAsync(IEnumerable<TEntity> entitycollection,CancellationToken cancellationToken=default);
        TEntity Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entitycollection); 
        void Delete(TEntity entity); 
        void DeleteRange(IEnumerable<TEntity> entitycollection);
    
    }
}
