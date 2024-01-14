using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting.Internal;
using venteEnligneApp.Models;
using venteEnligneApp.Repositories;
using venteEnligneApp.ViewModels;

namespace venteEnligneApp.Controllers
{
    [Authorize]
    public class ArticleController : Controller
    {

        readonly IArticleRepository ArticleRepository;
        readonly ICategoryRepository CategoryRepository;
        private readonly IWebHostEnvironment hostingEnvironment;


        public ArticleController(IArticleRepository articleRepository, ICategoryRepository categoryRepository, IWebHostEnvironment hostingEnvironment)
        {
            ArticleRepository = articleRepository;
            CategoryRepository = categoryRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: ArticleController
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var articles = await ArticleRepository.GetAllArticles();
            return View(articles);
        }

        // GET: ArticleController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var article = await ArticleRepository.GetArticleById(id);
            return View(article);
        }

        // GET: ArticleController/Create
        public async Task<IActionResult> Create()
        {
            var categories = await CategoryRepository.GetAllCategories();

            // Ensure that categories is not null and contains items
            if (categories != null && categories.Any())
            {
                ViewBag.Categories = new SelectList(categories, "Id", "Nom");
            }
            else
            {
                // Handle the case where no categories are available
                ViewBag.Categories = new SelectList(new List<Category>(), "Id", "Nom");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            

            Article newArticle = new Article

            {
                Nom = model.Nom,
                Description = model.Description,
                Image = ProcessUploadedFile(model),
            Prix = model.Prix,
                    Marque = model.Marque,
                    CategoryId = model.CategoryId,
                    Categorie = await CategoryRepository.GetCategoryById((int)model.CategoryId),
                    Quantite = model.Quantite,
                };
                await ArticleRepository.AddArticle(newArticle);
                return RedirectToAction("details", new { id = newArticle.Id });
           /* }
            // If the model state is not valid, reload the categories and return to the create view
            var categories = await CategoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Nom", model.CategoryId);
            return View();*/
        }

        // GET: ArticleController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var article = await ArticleRepository.GetArticleById(id);
            var categories = await CategoryRepository.GetAllCategories();

            // Ensure that categories is not null and contains items
            if (categories != null && categories.Any())
            {
                ViewBag.Categories = new SelectList(categories, "Id", "Nom");
            }
            else
            {
                // Handle the case where no categories are available
                ViewBag.Categories = new SelectList(new List<Category>(), "Id", "Nom");

            }
            EditViewModel articleEditViewModel = new EditViewModel
            {
                Nom = article.Nom,
                Description = article.Description,
                ExistingImagePath = article.Image,
                Prix = article.Prix,
                Marque = article.Marque,
                CategoryId = article.CategoryId,
                Categorie = await CategoryRepository.GetCategoryById((int)article.CategoryId),
                Quantite = article.Quantite,
            };
            return View(articleEditViewModel);
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel model)
        {
            // Check if the provided data is valid, if not rerender the edit view
            // so the user can correct and resubmit the edit form
            if (ModelState.IsValid)
            {
                // Retrieve the product being edited from the database
                var article = await ArticleRepository.GetArticleById(model.Id);
                // Update the product object with the data in the model object
                article.Nom = model.Nom;
                article.Description = model.Description;
                article.Prix = model.Prix;
                article.Marque = model.Marque;
                article.CategoryId = model.CategoryId;
                article.Categorie = await CategoryRepository.GetCategoryById((int)model.CategoryId);
                article.Quantite = model.Quantite;
                // If the user wants to change the photo, a new photo will be
                // uploaded and the Photo property on the model object receives
                // the uploaded photo. If the Photo property is null, user did
                // not upload a new photo and keeps his existing photo
                if (model.ImagePath != null)
                {
                    // If a new photo is uploaded, the existing photo must be
                    // deleted. So check if there is an existing photo and delete
                    if (model.ExistingImagePath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingImagePath);
                        System.IO.File.Delete(filePath);
                    }
                    // Save the new photo in wwwroot/images folder and update
                    // PhotoPath property of the product object which will be
                    // eventually saved in the database
                    article.Image = ProcessUploadedFile(model);
                }

                // Call update method on the repository service passing it the

                // product object to update the data in the database table
                await ArticleRepository.UpdateArticle(article);
                if (article != null)
                    return RedirectToAction("Index");
                else
                    return NotFound();

            }
            return View(model);
        }

        [NonAction]
        private string ProcessUploadedFile(CreateViewModel model)
        {
            string uniqueFileName = "";
            if (model.ImagePath != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImagePath.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        // GET: ArticleController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var article = await ArticleRepository.GetArticleById(id);
            return View(article);
        }

        // POST: ArticleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                await ArticleRepository.DeleteArticle(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
