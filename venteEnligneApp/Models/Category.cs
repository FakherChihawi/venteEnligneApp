namespace venteEnligneApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}
