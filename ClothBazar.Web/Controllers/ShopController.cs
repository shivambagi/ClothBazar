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

        public ActionResult Index(string searchTerm,int? minimumPrice,int? maximumPrice,int? categoryID,int? sortBy,int? pageNo)
        {
            ShopViewModel model = new ShopViewModel();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            int pageSize = 10;

            model.FeaturedCategories = categoryService.GetFeaturedCategories();
            model.MaximumPrice = productService.GetMaximumPrice();

            model.Products = productService.SearchProducts(searchTerm,minimumPrice,maximumPrice,categoryID, sortBy, pageNo.Value, pageSize);

            model.SortBy = sortBy;

            model.CategoryID = categoryID;

            int totalCount = productService.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value);

            model.Pager = new Pager(totalCount,pageNo);

            return View(model);
        }

        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            FilterProductsViewModel model = new FilterProductsViewModel();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            int pageSize = 10;

            model.Products = productService.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);

            int totalCount = productService.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value);

            model.Pager = new Pager(totalCount, pageNo);

            return PartialView(model);
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