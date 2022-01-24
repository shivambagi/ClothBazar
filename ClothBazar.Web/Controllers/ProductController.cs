using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductsService productsService = ProductsService.Instance; // you can create a single object and use it directly for calling class methods or follow below category service example
        //CategoriesService categoryService = new CategoriesService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search, int? pageNo)
        {
            ProductSearchViewModel model = new ProductSearchViewModel();

            //model.PageNo = pageNo.HasValue ? pageNo.Value : 1; //try in html by disabling button or /hide previous btn using condition
            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.Products = productsService.GetProducts(model.PageNo);
            if(string.IsNullOrEmpty(search) == false)
            {
                model.SearchTerm = search;
                model.Products = model.Products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }
            
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();

            //var categories = categoryService.GetCategories();
            model.AvailableCategories = CategoriesService.Instance.GetCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            //newProduct.CategoryID = model.CategoryID; //use this for not allowing duplicated and fewer calls to DB,need to add property in Product class approach 1
            newProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryID);
            newProduct.ImageURL = model.ImageURL;

            productsService.SaveProduct(newProduct);

            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();

            var product = productsService.GetProduct(ID);

            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.Category != null ? product.Category.ID : 0;
            model.AvailableCategories = CategoriesService.Instance.GetCategories();
            model.ImageURL = product.ImageURL;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = productsService.GetProduct(model.ID);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;
            existingProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryID);
            existingProduct.ImageURL = model.ImageURL;

            productsService.UpdateProduct(existingProduct);
            return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(int ID)
        {
            productsService.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }
    }
}