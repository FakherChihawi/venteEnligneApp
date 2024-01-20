using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using venteEnligneApp.Models;

namespace venteEnligneApp.Repositories
{
    public class CommandeRepository : ICommandeRepository
    {
        private readonly AppDbContext _context;

        public CommandeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCommande(Commande commande)
        {
            _context.Commandes.Add(commande);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommande(int commandeId)
        {
            var commande = await _context.Commandes.FindAsync(commandeId);
            if (commande != null)
            {
                _context.Commandes.Remove(commande);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Commande>> GetAllCommandes()
        {
            return await _context.Commandes.ToListAsync();
        }

        public async Task<Commande> GetCommandeById(int commandeId)
        {
            return await _context.Commandes.FindAsync(commandeId);
        }

        public async Task<IEnumerable<Commande>> GetCommandeByUserAndArticle(string userID, int articleId)
        {
            return await _context.Commandes
                            .Where(c => c.UserId == userID && c.ArticleId == articleId)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Commande>> GetCommandesByUserId(string userID)
        {
            return await _context.Commandes
               .Where(c => c.UserId == userID && !c.status)
               .ToListAsync();
        }

        public async Task<IEnumerable<Commande>> GetCommandesNonValide()
        {
            return await _context.Commandes
               .Where(c => !c.status)
               .ToListAsync();
        }

        public async Task UpdateCommande(Commande commande)
        {
            _context.Entry(commande).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public bool UserHasCommand(string userID, int articleId)
        {
            var hasCommand = _context.Commandes.Any(c => c.UserId == userID && c.ArticleId == articleId);
            return hasCommand;
        }
    }
}
