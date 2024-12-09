using JobExpressBack.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobExpressBack.Models.Entities
{
    public class DemandeService
    {
        // cle primaire composé de clientID et ProfessionnelId
        [Key, Column(Order = 0)]
        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }
        [Key, Column(Order = 1)]
        public string ProfessionnelId { get; set; }
        public ApplicationUser Professionnel { get; set; }
        [Key, Column(Order = 2)]
        public DateTime DateDemande { get; set; } = DateTime.Now;
        public Statuts Statut { get; set; }   // en_attente, acceptée, en_cours, terminée, annulée
        public DateTime? ExecutionDate { get; set; }
        public decimal TotalPrice { get; set; }

        // Propriété de navigation pour les avis
        public ICollection<Avis> Avis { get; set; }

        [Required]
        public int NotificationID { get; set; }  // Clé étrangère pour la relation One-to-One
        [ForeignKey("NotificationID")]
        public Notification Notification { get; set; }

        public enum Statuts
        {
            EnAttente,   // 0
            Acceptee,    // 1
            EnCours,     // 2
            Terminee,    // 3
            Annulee      // 4
        }
    }
}
