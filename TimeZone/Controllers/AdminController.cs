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
        public async Task<IActionResult> Products()
		{
			
			var products = await _unitOfWork.ProductRepository.GetAll();
			var ProductViewModel = _mapper.Map<List<ProductViewModel>>(products);

            return View(ProductViewModel); 
		}


		[HttpGet]
		public async Task<IActionResult> AddProduct() 
		{
			ViewData["Category"]=await _unitOfWork.CategoryRepository.GetAll();

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SaveAddedProduct(Product product , IFormFile Img)// addimage here
		{

            if (Img != null && Img.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    Img.CopyTo(stream);
                    product.Img = stream.ToArray();
                }
            }
            await _unitOfWork.ProductRepository.Add(product);
			_unitOfWork.Commit();
			return RedirectToAction("Products");
		}

		[HttpGet]
		public async Task<IActionResult> UpdateProduct(int Id) 
		{
			var product = await _unitOfWork.ProductRepository.GetById(Id);
			ViewData["Category"]= await _unitOfWork.CategoryRepository.GetAll();
            return View(product);
		}

		[HttpPost] 
		public async Task<IActionResult> SaveUpdatedProduct(Product product, IFormFile Img) 
		{ 
			// save only updated ones 
			var NotNullValues =await _unitOfWork.ProductRepository.GetById(product.Id);
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
        public  async Task<IActionResult> Categories()
        {
			 
            var categories =  await _unitOfWork.CategoryRepository.GetAll();
            var CategoryViewModel = _mapper.Map<List<CategoryViewModel>>(categories);

            return View(CategoryViewModel);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveAddedCategory(Category category)// addimage here
        {
           await _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.Commit();
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int Id)
        {   // here you dont need the list tho
            var category =  await _unitOfWork.CategoryRepository.GetById(Id);
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



        [HttpGet]
        public async Task<IActionResult> ShowFeedBack(int Id)
        {   // here you dont need the list tho
            var FeedBack = await _unitOfWork.FeedBackRepository.GetById(Id);
            //ViewData["Products"];
            return View(FeedBack);
        }
        public IActionResult DeleteFeedBack(FeedBack feedBack)
        {
            _unitOfWork.FeedBackRepository.Delete(feedBack);
            _unitOfWork.Commit();
            return RedirectToAction("FeedBack");
        }

        [HttpGet]
        public async Task<IActionResult> FeedBack()
        {

            var feedBacks = await _unitOfWork.FeedBackRepository.GetAll();

            return View(feedBacks);
        }
        #endregion
    }
}
