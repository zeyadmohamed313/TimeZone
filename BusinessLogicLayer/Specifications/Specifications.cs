using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Specifications
{
    public abstract class Specifications<TEntity> where TEntity : class
    {
        public Specifications() { }

        public Specifications(Expression<Func<TEntity,bool>>? criteria) 
        {
            Criteria = criteria;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; }

        public List<Expression<Func<TEntity, object>>>? Includes { get; } = new List<Expression<Func<TEntity, object>>>();

        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; private set; }
        public List<Expression<Func<TEntity, object>>>? ThenIncludes { get; } = new List<Expression<Func<TEntity, object>>>();


        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes?.Add(includeExpression);
        }
        protected void AddThenInclude(Expression<Func<TEntity, object>> thenIncludeExpression)
        {
            ThenIncludes?.Add(thenIncludeExpression);
        }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderExpression)
        {
            OrderBy = orderExpression;
        }

        protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderExpression)
        {
            OrderByDesc = orderExpression;
        }
    }
}

/*
  call a explict specification like price 
  give it the generic one 
  the generic one will call the builder
 
 */
