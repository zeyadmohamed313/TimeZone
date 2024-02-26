using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer;
using BusinessLogicLayer.UnitOfWork;
using DataAccessLayer.Models;
using static System.Net.Mime.MediaTypeNames;
using BusinessLogicLayer.ViewModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace PresentationLayer.Controllers
{
	public class AdminController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public AdminController(IUnitOfWork unitOfWork , IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public IActionResult Index()
		{
			return View();
		}
		#region Products
		[HttpGet]
        public IActionResult Products()
		{
			
			var products =_unitOfWork.ProductRepository.GetAll();
			var ProductViewModel = _mapper.Map<List<ProductViewModel>>(products);

            return View(ProductViewModel); 
		}
		[HttpGet]
		public IActionResult AddProduct() 
		{
			ViewData["Category"]=_unitOfWork.CategoryRepository.GetAll();

			return View();
		}

		[HttpPost]
		public IActionResult SaveAddedProduct(Product product , IFormFile Img)// addimage here
		{

            if (Img != null && Img.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    Img.CopyTo(stream);
                    product.Img = stream.ToArray();
                }
            }
            _unitOfWork.ProductRepository.Add(product);
			_unitOfWork.Commit();
			return RedirectToAction("Products");
		}

		[HttpGet]
		public IActionResult UpdateProduct(int Id) 
		{
			var product = _unitOfWork.ProductRepository.GetById(Id);
			ViewData["Category"]=_unitOfWork.CategoryRepository.GetAll();
            return View(product);
		}

		[HttpPost] 
		public IActionResult SaveUpdatedProduct(Product product, IFormFile Img) 
		{ 
			// save only updated ones 
			var NotNullValues = _unitOfWork.ProductRepository.GetById(product.Id);
			var img = NotNullValues.Img;
			// Leave Null Values
		    NotNullValues= _mapper.Map(product, NotNullValues);
			if (Img != null && Img.Length > 0)
			{
				using (var stream = new MemoryStream())
				{
					Img.CopyTo(stream);
					NotNullValues.Img = stream.ToArray();
				}
			}
			else NotNullValues.Img = img;
			_unitOfWork.ProductRepository.Update(NotNullValues);
			_unitOfWork.Commit();
			return RedirectToAction("Products");

        }

		public IActionResult DeleteProduct(Product product)
		{
			_unitOfWork.ProductRepository.Delete(product);
			_unitOfWork.Commit();
			return RedirectToAction("Products");
		}
        #endregion


        #region Category
        [HttpGet]
        public IActionResult Categories()
        {

            var categories = _unitOfWork.CategoryRepository.GetAll();
            var CategoryViewModel = _mapper.Map<List<CategoryViewModel>>(categories);

            return View(CategoryViewModel);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveAddedCategory(Category category)// addimage here
        {
            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.Commit();
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int Id)
        {
            var category = _unitOfWork.CategoryRepository.GetCategoryWithProducts(Id);
			//ViewData["Products"];
            return View(category);
        }

        [HttpPost]
        public IActionResult SaveUpdatedCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();
            return RedirectToAction("Categories");

        }

        public IActionResult DeleteCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Commit();
            return RedirectToAction("Categories");
        }
        #endregion
    }
}
