using AutoMapper;
using BusinessLogicLayer.Specifications;
using BusinessLogicLayer.UnitOfWork;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly StripeSettings _stripeSettings;
        public CartController(IUnitOfWork unitOfWork, IMapper mapper, IOptions<StripeSettings> stripeSettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _stripeSettings = stripeSettings.Value;   
        }
        public async Task<IActionResult> Index()
        {
            ViewData["SH"]= await GetUserShoppingCart();
            var x = ViewData["SH"];
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
            ViewData["SH"]=await GetUserShoppingCart();
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
        private async Task<ShoppingCart> GetUserShoppingCart()
        {
            var userId = GetCurrentUserId();
            var Specifications = new ShoppingCartWithUserId(userId);
            var ShoppingCart = await _unitOfWork.ShoppingCartRepository.GetWithSpecifications(Specifications);
            return ShoppingCart[0];
        }
        
        public IActionResult CreateCheckOutSession(string Total)
        {
            var currency = "usd";
            var successurl = "http://localhost:7279/";
            var failurl = "http://localhost:7279/";
            StripeConfiguration.ApiKey = _stripeSettings.Secretkey;

            var option = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            UnitAmount = (long?)(Convert.ToDecimal(Total) * 100),
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "ProductName",
                                Description = "ProductDescreption"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successurl,
                CancelUrl = failurl,
            };
            var service = new SessionService();
            var session = service.Create(option);
            return Redirect(session.Url);
        }
        public async Task<IActionResult>Success()
        {
            return View("Index");
        }
        public IActionResult Cancel()
        {
            return RedirectToAction("Index");
        }
    }
}
