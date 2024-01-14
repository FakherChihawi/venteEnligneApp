using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using venteEnligneApp.Models;
using venteEnligneApp.Repositories;

namespace venteEnligneApp.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        readonly ICategoryRepository CategoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        // GET: CategorieController
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var categories = await CategoryRepository.GetAllCategories();
            return View(categories);
        }


        // GET: CategorieController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await CategoryRepository.GetCategoryById(id);

            return View(category);
        }

        // GET: CategorieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategorieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            var category = new Category();
            category.Nom = collection["Nom"];
            category.Description = collection["Description"];
            try
            {
                await CategoryRepository.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: CategorieController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var category = await CategoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategorieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                var category =  CategoryRepository.GetCategoryById(id).Result;
                if (category == null)
                {
                    return NotFound();
                }
                category.Nom = collection["Nom"];
                category.Description = collection["Description"];

                await CategoryRepository.UpdateCategory(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CategorieController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var category = await CategoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategorieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await CategoryRepository.DeleteCategory(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
