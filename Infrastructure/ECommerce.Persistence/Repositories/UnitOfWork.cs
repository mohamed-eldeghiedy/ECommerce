using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistence.Repositories
{
    internal class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {

        private readonly Dictionary<string, Object> _repositories =[];
        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : Entity<TKey>
        {
            var type = typeof(TEntity).Name;
            if (_repositories.ContainsKey(type))
            {
                return(_repositories[type] as IRepository<TEntity , TKey>)!;
            }
            var repo = new Repository<TEntity, TKey>(dbContext);
            _repositories.Add(type, repo);
            return repo;
        }

        public Task<int> SaveChengesAsync(CancellationToken cancellationToken = default)
            => dbContext.SaveChangesAsync(cancellationToken);

    }
}
