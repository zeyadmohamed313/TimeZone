using AutoMapper;
using BusinessLogicLayer.Specifications;
using BusinessLogicLayer.UnitOfWork;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CartController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await GetAllItemsInMyCart());
        }
        
        public async Task<IActionResult> AddToCart(int id)
        {
             var userId = GetCurrentUserId();
             await _unitOfWork.ShoppingCartRepository.AddToCart(userId,id);
             _unitOfWork.Commit();
             return RedirectToAction("Index");   
        }

        private string GetCurrentUserId()
        {
           return (User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public async Task <IActionResult> DeletefromCart(int ProductId)
        {
            var userId = GetCurrentUserId();
            await _unitOfWork.ShoppingCartRepository.DeleteFromCart(userId, ProductId);
            _unitOfWork.Commit();
            return PartialView("_CartBody", await GetAllItemsInMyCart());
        }
        private async Task<List<ProductViewModel>> GetAllItemsInMyCart()
        {
            var userId = GetCurrentUserId();
            var Specifications = new ShoppingCartWithUserId(userId);
            var ShoppingCart = await _unitOfWork.ShoppingCartRepository.GetWithSpecifications(Specifications);
            var products = ShoppingCart[0].OrderList.ToList(); // First Shopping Cart
            var ProductVm = _mapper.Map<List<ProductViewModel>>(products);
            return ProductVm;
        }
    }
}
