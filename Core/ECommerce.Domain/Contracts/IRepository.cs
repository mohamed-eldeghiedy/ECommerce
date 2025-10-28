using ECommerce.Domain.Entities;
using ECommerce.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Contracts
{
    public interface IRepository<TEntity , TKey>
        where TEntity : Entity<TKey> 
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);

        Task<TEntity?> GetByIdAsync(TKey id , CancellationToken cancellationToken);
        Task<TEntity?> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        Task<IEnumerable<TEntity>> GetAllAsync( ISpecification<TEntity> specification,CancellationToken cancellationToken);

        Task<int> CountAsync( ISpecification<TEntity> specification , CancellationToken cancellationToken);
    }
}
