using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        ProductsService productService = ProductsService.Instance;
        CategoriesService categoryService = CategoriesService.Instance;
        // GET: Shop

        public ActionResult Index(string searchTerm,int? minimumPrice,int? maximumPrice,int? categoryID,int? sortBy,int? pageNo)
        {
            ShopViewModel model = new ShopViewModel();

            model.SearchTerm = searchTerm;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            int pageSize = int.Parse(ConfigurationsService.Instance.GetConfig("ShopPageSize").Value);
            model.SortBy = sortBy;
            model.CategoryID = categoryID;
            model.FeaturedCategories = categoryService.GetFeaturedCategories();
            model.MaximumPrice = productService.GetMaximumPrice();

            model.Products = productService.SearchProducts(searchTerm,minimumPrice,maximumPrice,categoryID, sortBy, pageNo.Value, pageSize);            

            int totalCount = productService.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value);

            model.Pager = new Pager(totalCount,pageNo, pageSize);

            return View(model);
        }

        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            FilterProductsViewModel model = new FilterProductsViewModel();

            model.SearchTerm = searchTerm;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            int pageSize = int.Parse(ConfigurationsService.Instance.GetConfig("ShopPageSize").Value);
            model.SortBy = sortBy;
            model.CategoryID = categoryID;

            model.Products = productService.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);

            int totalCount = productService.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value);

            model.Pager = new Pager(totalCount, pageNo, pageSize);

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