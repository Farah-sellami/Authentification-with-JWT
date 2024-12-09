using JobExpressBack.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobExpressBack.Models.Entities
{
    public class Avis
    {
        public int AvisID { get; set; }
        public string Comment { get; set; }
        [Range(1, 5)]
        public int Rate { get; set; } // Valeur entre 1 et 5
        public DateTime DateAvis { get; set; } = DateTime.Now;

        // Clés étrangères vers DemandeService
        public string ClientId { get; set; }
        public string ProfessionnelId { get; set; }
        public DateTime DateDemande { get; set; }

        [ForeignKey("ClientId, ProfessionnelId,DateDemande")] // Clé composite
        public DemandeService DemandeService { get; set; }


    }
}
