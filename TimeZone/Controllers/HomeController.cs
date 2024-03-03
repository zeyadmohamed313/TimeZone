using AutoMapper;
using BusinessLogicLayer.UnitOfWork;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TimeZone.Controllers
{
	[AllowAnonymous]
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

		//[ResponseCache(Duration = 3600)]
		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var PopularProducts = await _unitOfWork.ProductRepository.GetPopularProducts();
			var PopularProductsVM = _mapper.Map<List<ProductViewModel>>(PopularProducts);
			return View(PopularProductsVM);
		}

		public IActionResult Privacy()
		{
			return View();
		}

	}
}