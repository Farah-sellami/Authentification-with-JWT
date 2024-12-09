using JobExpressBack.Models.DTOs;
using JobExpressBack.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace JobExpressBack.Models

{
    public class ExJobDBContext : IdentityDbContext<ApplicationUser>
    {
        public ExJobDBContext(DbContextOptions<ExJobDBContext> options) : base(options)
        {

        }

        public DbSet<Avis> Avis { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<DemandeService> DemandeServices { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Service> Services { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration explicite des relations pour éviter les ambiguïtés dans messages
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.MessagesSent)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Recipient)
                .WithMany(u => u.MessagesReceived)
                .HasForeignKey(m => m.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuration de la clé primaire composée de Demande Service
            modelBuilder.Entity<DemandeService>()
                .HasKey(ds => new { ds.ClientId, ds.ProfessionnelId , ds.DateDemande});

            // Relation Client -> DemandeService
            modelBuilder.Entity<DemandeService>()
                .HasOne(ds => ds.Client)
                .WithMany(u => u.ServicesRequested)
                .HasForeignKey(ds => ds.ClientId)
                .OnDelete(DeleteBehavior.Restrict); // Évite les suppressions en cascade indésirables

            // Relation Professionnel -> DemandeService
            modelBuilder.Entity<DemandeService>()
                .HasOne(ds => ds.Professionnel)
                .WithMany(u => u.ServicesProvided)
                .HasForeignKey(ds => ds.ProfessionnelId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configurer la relation entre Avis et DemandeService
            modelBuilder.Entity<Avis>()
                .HasOne(a => a.DemandeService)
                .WithMany(d => d.Avis)
                .HasForeignKey(a => new { a.ClientId, a.ProfessionnelId , a.DateDemande })
                .OnDelete(DeleteBehavior.Restrict);  // Modifier le comportement en fonction des besoins

            // Configurer la relation One-to-One entre DemandeService et Notification
            modelBuilder.Entity<DemandeService>()
                .HasOne(ds => ds.Notification)
                .WithOne(n => n.Demande)
                .HasForeignKey<DemandeService>(ds => ds.NotificationID)
                .OnDelete(DeleteBehavior.Cascade);  // ou DeleteBehavior.Restrict en fonction du comportement souhaité

            // Relation One-to-Many entre Service et ApplicationUser
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Service)
                .WithMany(s => s.Professionnels)
                .HasForeignKey(u => u.ServiceID)
                .OnDelete(DeleteBehavior.Restrict); // Empêche la suppression d'un Service si des Professionnels y sont liés

        }
    }
}
