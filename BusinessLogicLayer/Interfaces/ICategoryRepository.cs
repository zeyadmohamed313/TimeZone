using BusinessLogicLayer.Base;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
	public interface ICategoryRepository:IGenericRepository<Category>
	{
		Category GetCategoryWithProducts(int id);
	}
}
