using JobExpressBack.Models.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobExpressBack.Models.Entities
{
    public class Service
    {
        public int ServiceID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
       
        public DateTime DateCreation { get; set; }

        // Navigation properties
        [ForeignKey("CategorieID")]
        public int CategorieID { get; set; }
        public Categorie Categorie { get; set; }
      
        public ICollection<ApplicationUser> Professionnels { get; set; }
    }
}
