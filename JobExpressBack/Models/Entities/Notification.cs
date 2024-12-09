using JobExpressBack.Models.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobExpressBack.Models.Entities
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
        public DemandeService Demande { get; set; }
    }
}
