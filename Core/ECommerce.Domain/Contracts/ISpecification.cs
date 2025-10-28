using ECommerce.Domain.Entities;
using System.Linq.Expressions;

namespace ECommerce.Persistence.Repositories
{
    public interface ISpecification<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
       ICollection<Expression<Func<TEntity , Object>>> Includes { get;  }

        Expression<Func<TEntity, Object>> OrderBy { get;  }
        Expression<Func<TEntity, Object>> OrderByDescending { get; }

        int Skip { get; }
        int Take { get; }
        bool IsPagingEnabled { get; }


    }
}