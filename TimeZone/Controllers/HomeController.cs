using BusinessLogicLayer.UnitOfWork;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TimeZone.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IUnitOfWork _unitOfWork;
		public HomeController(ILogger<HomeController> logger
			, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			var products = _unitOfWork.ProductRepository.GetAll();
			return View(products);
		}

		public IActionResult Privacy()
		{
			return View();
		}

	}
}