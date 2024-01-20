using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using venteEnligneApp.Models;

namespace venteEnligneApp.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddArticle(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteArticle(int articleId)
        {
            var article = await _context.Articles.FindAsync(articleId);
            if (article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Article>> GetAllArticles()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task<Article> GetArticleById(int articleId)
        {
            return await _context.Articles.FindAsync(articleId);
        }

        public async Task<IEnumerable<Article>> GetArticlesByCategory(int categoryId)
        {
            return await _context.Articles
                .Where(a => a.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task UpdateArticle(Article article)
        {
            _context.Entry(article).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Article>> GetArticlesByMarque(string marque)
        {
            return await _context.Articles
                .Where(a => a.Marque == marque)
                .ToListAsync();
        }

        public async Task<IEnumerable<Article>> GetArticlesByMarqueAndCategory(string marque, int categoryId)
        {
            return await _context.Articles
                .Where(a => a.Marque == marque && a.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
