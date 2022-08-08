using System;
using ClothBazar.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Entities;
using ClothBazar.Web.ViewModels;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductsService productService = new ProductsService();
        CategoriesService category = new CategoriesService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search)
        {
            var products = productService.GetProducts();

            if (string.IsNullOrEmpty(search) == false)
            {
                products = products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();

            }

            return PartialView(products);
        }

        [HttpGet]
        public ActionResult Create()
        {

            var categories = category.GetCategories();

            return PartialView(categories);
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            newProduct.Category = category.GetCategory(model.CategoryID);

            productService.SaveProduct(newProduct);

            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var product = productService.GetProduct(ID);

            return PartialView(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productService.UpdateProduct(product);

            return RedirectToAction("ProductTable");
        }

        
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            productService.DeleteProduct(ID);

            return RedirectToAction("ProductTable");
        }
    }
}