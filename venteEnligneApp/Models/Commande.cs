namespace venteEnligneApp.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ArticleId { get; set; }
        public bool status { get; set; }
    }
}
