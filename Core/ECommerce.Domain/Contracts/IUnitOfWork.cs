using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface IUnitOfWork
    {

        Task<int> SaveChengesAsync(CancellationToken cancellationToken = default);

        IRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : Entities.Entity<TKey>;
    }
}
