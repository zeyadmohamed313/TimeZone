using BusinessLogicLayer.Specifications;
using BusinessLogicLayer.UnitOfWork;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class ShopController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShopController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var products = _unitOfWork.ProductRepository.GetAll();
            ViewData["Categories"] = _unitOfWork.CategoryRepository.GetAll();

            return View(products);
        }

        [HttpGet]
        public IActionResult FilterProducts(int? CategoryId , decimal? MinPrice ,decimal? MaxPrice 
            ,bool SortAsc = true)
        {
            Specifications<Product> specifications;

            if (SortAsc == true)
                specifications = new FilterProductsAsc(CategoryId,MinPrice,MaxPrice);
            else
                specifications = new FilterProductsDesc(CategoryId, MinPrice, MaxPrice);

            var FilteredProducts = _unitOfWork.ProductRepository.GetWithSpecifications(specifications);

            return PartialView("_filterProducts",FilteredProducts);
        }
    }
}
