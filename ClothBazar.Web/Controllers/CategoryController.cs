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
    public class CategoryController : Controller
    {
        // GET: Category

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #region CategoryTable
        public ActionResult CategoryTable(string search)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();
            model.Categories = CategoriesService.Instance.GetCategories();

            if (string.IsNullOrEmpty(search) == false)
            {
                model.SearchTerm = search;
                model.Categories = model.Categories.Where(c => c.Name != null && c.Name.ToLower().Contains(search.ToLower())).ToList();

            }

            return PartialView("Categorytable", model);
        }
        #endregion

        #region Creation
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category();
                newCategory.Name = model.Name;
                newCategory.Description = model.Description;
                newCategory.ImageUrl = model.Description;
                newCategory.isFeatured = model.isFeatured;

                CategoriesService.Instance.SaveCategory(newCategory);

                return RedirectToAction("CategoryTable");
            }

            else
            {
                return new HttpStatusCodeResult(500);
            }
        }
        #endregion

        #region Updation
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();
            var category = CategoriesService.Instance.GetCategory(ID);

            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageUrl;
            model.isFeatured = category.isFeatured;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var existingCategory = CategoriesService.Instance.GetCategory(model.ID);

            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageUrl = model.ImageURL;
            existingCategory.isFeatured = model.isFeatured;

            CategoriesService.Instance.UpdateCategory(existingCategory);

            return RedirectToAction("CategoryTable");
        }
        #endregion

        #region Deletion
        [HttpPost]
        public ActionResult Delete(Category category)
        {

            CategoriesService.Instance.DeleteCategory(category.ID);

            return RedirectToAction("Index");
        }
        #endregion

    }
}