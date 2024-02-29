

using BusinessLogicLayer.Specifications;
using DataAccessLayer.Context;
using System.Data.Entity;

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

		public  IEnumerable<T>? GetAll()
		{
			return  _appContext.Set<T>().ToList();
		}

		public async Task Update(T entity)
		{
			 _appContext.Update<T>(entity);
		}
		public  T? GetById(int id)
		{
			return  _appContext.Set<T>().Find(id);
		}

        public List<T> GetWithSpecifications(Specifications<T>? specifications)
		{
			return SpecificationQueryBuilder.GetQuery(_appContext.Set<T>(),specifications).ToList();
		}

    }
}
