using JobExpressBack.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobExpressBack.Models.DTOs
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50), Required]
        public string FirstName { get; set; }

        [MaxLength(50), Required]
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? Telephone { get; set; }
        public string? PhotoProfile { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true; // Indique si l'utilisateur est actif
        public DateTime? LastLoginDate { get; set; } // Stocke la date et l'heure de la dernière connexion
        public string? Skills { get; set; } // Liste des compétences sous forme de texte
        public string? Disponibility { get; set; } // Exemple : JSON décrivant le calendrier


        // Statistiques pour les professionnels
        public int ServicesProvidedCount { get; set; }
        public int YearsOfExperience { get; set; } // Nombre d'années d'expérience
        

        // Navigation properties
        //many to many avec demande service

        public ICollection<DemandeService> ServicesRequested { get; set; } // Services demandés par le client
        public ICollection<DemandeService> ServicesProvided { get; set; } // Services fournis par le professionnel
        public ICollection<Message> MessagesSent { get; set; } // Messages envoyés
        public ICollection<Message> MessagesReceived { get; set; } // Messages reçus

        // Relation Many-to-One prof avec Service
  
        public int? ServiceID { get; set; } // Clé étrangère vers Service
        public Service Service { get; set; }
    }
}
