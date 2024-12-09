using System.ComponentModel.DataAnnotations.Schema;

namespace JobExpressBack.Models.Entities
{
    public class Categorie
    {
        public int CategorieID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Navigation property
        public ICollection<Service> Services { get; set; }
    }
}
