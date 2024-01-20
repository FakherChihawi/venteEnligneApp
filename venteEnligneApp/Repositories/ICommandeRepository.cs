using System.Threading.Tasks;
using venteEnligneApp.Models;

namespace venteEnligneApp.Repositories
{
    public interface ICommandeRepository
    {
        Task<IEnumerable<Commande>> GetAllCommandes();
        Task<IEnumerable<Commande>> GetCommandesByUserId(string userID);
        Task<IEnumerable<Commande>> GetCommandesNonValide();
        Task<Commande> GetCommandeById(int commandeId);
        Task<IEnumerable<Commande>> GetCommandeByUserAndArticle(string userID, int articleId);
        Task AddCommande(Commande commande);
        Task UpdateCommande(Commande commande);
        Task DeleteCommande(int commandeId);
        bool UserHasCommand(string userID , int articleId);
    }
}
