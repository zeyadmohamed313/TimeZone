

using BusinessLogicLayer.Specifications;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Base
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly AppDbContext _appContext;
		public GenericRepository(AppDbContext appContext)
		{
			_appContext = appContext;
		}

		public async Task Add(T entity)
		{
			await _appContext.AddAsync<T>(entity);
		}

		public  void Delete(T entity)
		{
			 _appContext.Remove<T>(entity);
		}

		public async Task<IEnumerable<T>>? GetAll()
		{
			return await _appContext.Set<T>().ToListAsync();
		}

		public async Task Update(T entity)
		{
			 _appContext.Update<T>(entity);
		}
		public async Task<T>? GetById(int id)
		{
			return await _appContext.Set<T>().FindAsync(id);
		}

        public async Task<List<T>>? GetWithSpecifications(Specifications<T>? specifications)
		{
            return await SpecificationQueryBuilder.GetQuery(_appContext.Set<T>(), specifications).ToListAsync();
		}

    }
}
