using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using venteEnligneApp.Repositories;

namespace venteEnligneApp.Controllers
{
    public class CommandeController : Controller
    {

        readonly ICommandeRepository CommandeRepository;

        public CommandeController(ICommandeRepository commandeRepository)
        {
            CommandeRepository = commandeRepository;
        }

        // GET: CommandeController
        public async Task<ActionResult> Index()
        {
            var commandes = await CommandeRepository.GetAllCommandes();
            return View(commandes);
        }

        // GET: CommandeController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var commande = await CommandeRepository.GetCommandeById(id);

            return View(commande);
        }

        // GET: CommandeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommandeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommandeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CommandeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommandeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CommandeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
