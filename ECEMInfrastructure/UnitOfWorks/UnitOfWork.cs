using ECEMInfrastructure.Repositories;
using ECMDomain.Abstraction;
using Microsoft.EntityFrameworkCore;
namespace ECEMInfrastructure.UnitOfWorks
{
    public class UnitOfWork(AppDbContext context) 
    {
        private readonly AppDbContext _context = context;

        public async Task<string> CommitAsync(CancellationToken cancellationToken = default, bool chekForConcurrency = false)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)when (chekForConcurrency)
            {
                return "A ConCurrency Conflict while saving changes";
            }
                return string.Empty;
         
        }
        //// This Form Before Make Generic repostory Inplement
        //public IGenericRepostiry<TEntity> Repostiry<TEntity>() where TEntity : BaseEntity
        //{ 
        //    throw new NotImplementedException();
        //} 
        //// This Form After Make Generic repostory Inplement 
        public IGenericRepostiry<TEntity> Repostiry<TEntity>() where TEntity : BaseEntity
        => new GenericRepository<TEntity>(_context);

    }
    //public class UnitOfWork(AppDbContext context) : IUnitWork
    //{
    //    private readonly AppDbContext _context = context;

    //    public async Task CommitAsync(CancellationToken cancellationToken = default, bool chekForConcurrency = false)
    //    {
    //        try
    //        {
    //            await _context.SaveChangesAsync(cancellationToken);
    //        }
    //        catch (DbUpdateConcurrencyException)when (chekForConcurrency)
    //        {
    //            throw new ConcurrencyException(["A ConCurrency Conflict while saving changes"]);
    //        } 

    //    }

   
}
