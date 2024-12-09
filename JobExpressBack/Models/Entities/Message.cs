using JobExpressBack.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobExpressBack.Models.Entities
{
    public class Message
    {
        public int MessageID { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }

        // Navigation
        // Expéditeur
        [Required]
        public string SenderId { get; set; }
        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; }

        // Destinataire
        [Required]
        public string RecipientId { get; set; }
        [ForeignKey("RecipientId")]
        public ApplicationUser Recipient { get; set; }

    }
}
