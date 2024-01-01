using venteEnligneApp.Models;

namespace venteEnligneApp.Repositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllArticles();
        Task<Article> GetArticleById(int articleId);
        Task<IEnumerable<Article>> GetArticlesByCategory(int categoryId);
        Task AddArticle(Article article);
        Task UpdateArticle(Article article);
        Task DeleteArticle(int articleId);
    }
}
