using venteEnligneApp.Models;

namespace venteEnligneApp.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int categoryId);
        Task<IEnumerable<Category>> GetCategoriesWithArticle();
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int categoryId);
    }
}
