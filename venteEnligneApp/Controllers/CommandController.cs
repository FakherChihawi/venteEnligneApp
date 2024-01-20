using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using venteEnligneApp.Models;
using venteEnligneApp.Repositories;
using venteEnligneApp.ViewModels;

namespace venteEnligneApp.Controllers
{
    public class CommandController : Controller
    {

        readonly ICommandeRepository CommandeRepository;
        readonly IArticleRepository ArticleRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;


        public CommandController(ICommandeRepository commandeRepository, IArticleRepository articleRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            CommandeRepository = commandeRepository;
            ArticleRepository = articleRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: CommandController
        public async Task<ActionResult> Index()
        {
            var commandes = await CommandeRepository.GetCommandesNonValide();


            // Create a list to hold the view model
            var commandDetails = new List<CommandDetailViewModel>();

            // Iterate through each command to retrieve user and article names
            foreach (var commande in commandes)
            {
                var user = await userManager.FindByIdAsync(commande.UserId);
                var article = await ArticleRepository.GetArticleById(commande.ArticleId);

                // Create a view model to hold the information
                var commandDetail = new CommandDetailViewModel
                {
                    CommandId = commande.Id,
                    UserName = user.UserName,
                    ArticleName = article.Nom,
                    Status = commande.status
                };

                // Add the view model to the list
                commandDetails.Add(commandDetail);
            }

            // Pass the list of command details to the view
            return View(commandDetails);
        }

        // GET: CommandController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var commande = await CommandeRepository.GetCommandeById(id);

            var user = await userManager.FindByIdAsync(commande.UserId);
            var article = await ArticleRepository.GetArticleById(commande.ArticleId);

            // Create a view model to hold the information
            var commandDetail = new CommandDetailViewModel
            {
                CommandId = commande.Id,
                UserName = user.UserName,
                ArticleName = article.Nom,
                Status = commande.status
            };

            return View(commandDetail);
        }

        // GET: CommandController/ValiderCommande/5
        public async Task<ActionResult> ValiderCommande(int id)
        {
            var commande = await CommandeRepository.GetCommandeById(id);
            commande.status = true;
            await CommandeRepository.UpdateCommande(commande);
            return RedirectToAction("Index");
        }

    }
}