using AutoMapper;
using BusinessLogicLayer.UnitOfWork;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TimeZone.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public HomeController(ILogger<HomeController> logger
			, IUnitOfWork unitOfWork, IMapper mapper)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public IActionResult Index()
		{
			var PopularProducts = _unitOfWork.ProductRepository.GetPopularProducts();
		    var PopularProductsVM = _mapper.Map<List<ProductViewModel>>(PopularProducts);
			return View(PopularProducts);
		}

		public IActionResult Privacy()
		{
			return View();
		}

	}
}