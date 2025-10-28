using ECommerce.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specification
{
    internal abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
        where TEntity : class
    {
        protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        public ICollection<Expression<Func<TEntity, object>>> Includes { get; private set; } = [];

        public Expression<Func<TEntity, bool>> Criteria{ get; protected set; }

        public Expression<Func<TEntity, object>> OrderBy { get; protected set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; protected set; }

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> expression) => OrderBy = expression;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> expression) => OrderByDescending = expression;


        public int Skip { get; protected set; }
        public int Take { get; protected set; }
        public bool IsPagingEnabled { get; protected set; }


        protected void ApplyPaging(int pageSize, int pageIndex)
        {
            
            IsPagingEnabled = true;
            Skip = pageSize * (pageIndex - 1);
            Take = pageSize;
        }
    }
}
