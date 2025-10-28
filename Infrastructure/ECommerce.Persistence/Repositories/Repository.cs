using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repositories
{
    internal class Repository<TEntity, TKey>(ApplicationDbContext dbContext)
        : IRepository<TEntity, TKey>
        where TEntity : Entity<TKey>
    {

        private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();


        public void Add(TEntity entity) 
            =>_dbSet.Add(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
            => await _dbSet
            .ToListAsync(cancellationToken);


        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken)
            => await _dbSet
            .FindAsync(id , cancellationToken);



        public void Remove(TEntity entity)
            => _dbSet.Remove(entity);

        public void Update(TEntity entity) 
            => _dbSet.Update(entity);



        public async Task<IEnumerable<TEntity>> GetAllAsync( ISpecification<TEntity> specification
            , CancellationToken cancellationToken)
        {
            return await _dbSet.ApplySpecification(specification)
                    .ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> GetAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            return await _dbSet
                .ApplySpecification(specification)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<int> CountAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
           return _dbSet
                .ApplySpecification(specification)
                .CountAsync(cancellationToken);
        }
    }
}
