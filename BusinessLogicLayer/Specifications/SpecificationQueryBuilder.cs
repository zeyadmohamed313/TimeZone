using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Specifications
{
    public static class SpecificationQueryBuilder
    {
        public static IQueryable<TEntity>? GetQuery<TEntity>(
            IQueryable<TEntity>? InputQuery , Specifications<TEntity> specifications )
            where TEntity : class
        {
            var query = InputQuery;

            if( specifications.Criteria != null )
            {
                // Criteria is used with where 
                query = query.Where(specifications.Criteria);

            }



            if (specifications.Includes != null)
            {
                query = specifications.Includes.Aggregate(query, (Current, Include) => Current.Include(Include));
                // Current is the query and include is the elements of the list (Includes) he iterate on it and add it one by one 
            }

            
          

            if (specifications.OrderBy!=null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }

            if (specifications.OrderByDesc != null)
            {
                query = query.OrderBy(specifications.OrderByDesc);
            }

            return query;
        }
    }
}
