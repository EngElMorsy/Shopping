using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECMDomain.Abstraction
{
    public interface IUnitWork
    {
        Task CommitAsync(CancellationToken cancellationToken=default,
            bool chekForConcurrency=false);
        IGenericRepostiry<TEntity> Repostiry<TEntity>()
            where TEntity : BaseEntity;

    }
}
