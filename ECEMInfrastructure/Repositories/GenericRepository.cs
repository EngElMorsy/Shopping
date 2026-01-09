using ECMDomain.Abstraction;
using Microsoft.EntityFrameworkCore;
namespace ECEMInfrastructure.Repositories
{
    public class GenericRepository<TEntity>(AppDbContext context) : IGenericRepostiry<TEntity> where TEntity :BaseEntity
    {
        private readonly AppDbContext _context = context;

        public IQueryable<TEntity> GetAll()
        => _context
            .Set<TEntity>()
            .AsNoTracking()
            .AsQueryable();

        public async Task<TEntity?> GetIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _context
            .Set<TEntity>()
            .FindAsync([id, cancellationToken],cancellationToken);


        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
               await _context
                .Set<TEntity>()
                .AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> entitycollection, CancellationToken cancellationToken = default)
        => await _context
                 .Set<TEntity>()
                 .AddRangeAsync(entitycollection, cancellationToken);
        
        public TEntity Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            return entity;
        }

        public void UpdateRange(IEnumerable<TEntity> entitycollection)
        => _context
            .Set<TEntity>()
            .UpdateRange(entitycollection);

        public void Delete(TEntity entity)
        => _context
            .Set<TEntity>()
            .Remove(entity);

        public void DeleteRange(IEnumerable<TEntity> entitycollection)
         => _context.Set<TEntity>().RemoveRange(entitycollection);
    }
}
