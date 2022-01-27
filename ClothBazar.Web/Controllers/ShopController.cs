using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ShopController : Controller
    {
        ProductsService productService = ProductsService.Instance;
        CategoriesService categoryService = CategoriesService.Instance;
        // GET: Shop

        public ActionResult Index(string searchTerm,int? minimumPrice,int? maximumPrice,int? categoryID,int? sortBy)
        {
            ShopViewModel model = new ShopViewModel();

            model.FeaturedCategories = categoryService.GetFeaturedCategories();
            model.MaximumPrice = productService.GetMaximumPrice();

            model.Products = productService.SearchProducts(searchTerm,minimumPrice,maximumPrice,categoryID, sortBy);

            model.SortBy = sortBy;

            return View(model);
        }

        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            var CartProductsCookie = Request.Cookies["CartProducts"];
            if(CartProductsCookie != null)
            {
                //var productIDs = CartProductsCookie.Value;
                //var ids = productIDs.Split('-');
                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();
                ////above thing done in one line below

                model.CartProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = productService.GetProducts(model.CartProductIDs);
            }

            return View(model);
        }
    }
}