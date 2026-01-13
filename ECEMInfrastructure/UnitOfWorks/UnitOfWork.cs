using ECEMInfrastructure.Repositories;
using ECMDomain.Abstraction;
using ECMDomain.Exceptions;
using Microsoft.EntityFrameworkCore;
namespace ECEMInfrastructure.UnitOfWorks
{
    public class UnitOfWork(AppDbContext context) : IUnitWork
    {
        private readonly AppDbContext _context = context;


        //** Before USE Custom Error Exception REturn type
        // public async Task<string> CommitAsync(CancellationToken cancellationToken = default, bool chekForConcurrency = false)
        public async Task CommitAsync(CancellationToken cancellationToken = default, bool chekForConcurrency = false) 
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)when (chekForConcurrency)
            { 
                //**BeFore Cutom Expection Type
               // return "A ConCurrency Conflict while saving changes"; 

                throw new ConcurrencyException(["A ConCurrency Conflict while saving changes"]);
            }
            //**BeFore Cutom Expection Type
          // return string.Empty;
         
        }
        //** This Form Before Make Generic repostory Inplement
       
         //public IGenericRepostiry<TEntity> Repostiry<TEntity>() where TEntity : BaseEntity
         //{ 
         //    throw new NotImplementedException();
         //} 
         //** This Form After Make Generic repostory Inplement 
        public IGenericRepostiry<TEntity> Repostiry<TEntity>() where TEntity : BaseEntity
        => new GenericRepository<TEntity>(_context);

      
    }
   
}
