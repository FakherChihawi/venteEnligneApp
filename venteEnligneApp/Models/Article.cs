﻿using System.ComponentModel.DataAnnotations;

namespace venteEnligneApp.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        [Display(Name = "Description :")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Image :")]
        public string Image { get; set; }
        public decimal Prix { get; set; }
        public string Marque { get; set; }
        public int? CategoryId { get; set; }
        public Category Categorie { get; set; }
        public int Quantite { get; set; }
    }
}
