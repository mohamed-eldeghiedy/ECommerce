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
        public void Add(TEntity entity) 
            =>dbContext.Set<TEntity>().Add(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
            => await dbContext.Set<TEntity>()
            .ToListAsync(cancellationToken);


        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken)
            => await dbContext.Set<TEntity>()
            .FindAsync(id , cancellationToken);



        public void Remove(TEntity entity)
            => dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
            => dbContext.Set<TEntity>().Update(entity);

    }
}
