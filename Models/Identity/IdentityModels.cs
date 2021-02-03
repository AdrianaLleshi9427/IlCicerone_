using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace IlCicerone.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string Name { get; set; }
        public string Surname { get; set; }



        //foreign keys
        public int? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }


        //country
        public int? LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public Language Language { get; set; }

        //city
        public int? CityId { get; set; }

        [ForeignKey("CityId")]
        public City city { get; set; }



        public string Gender { get; set; }

        //[DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
        //    ApplyFormatInEditMode = true)]
        //public DateTime Birthday { get; set; }

        public int YearlyTours { get; set; }

        public int DistancePreferences { get; set; }

        public int GroupSize { get; set; }

        public bool CityOrNature { get; set; }

        public bool SeaOrMountain { get; set; }

        public bool UseOwnVehicle { get; set; }

        public string PreferedTrasport { get; set; }

        public float? BudgetLimit { get; set; }

        public int TourDuration { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<RoleAccess> RoleAccesses { get; set; }
    }

    public class RoleAccess
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string RoleId { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {    
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Carousel> Carousels { get; set; }
        public DbSet<Partecipant> Partecipants { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<RoleAccess> RoleAccesses { get; set; }
        public DbSet<TourRating> TourRatings { get; set; }
        public DbSet<TourReview> Reviews { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<TourCollection> TourCollections { get; set; }
        public DbSet<ArticleCollection> ArticleCollections { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<demoEntities>(null);
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<Partecipant>().ToTable("Partecipants");
            modelBuilder.Entity<Feedback>().ToTable("Feedbacks");
            modelBuilder.Entity<Carousel>().ToTable("Carousels");
            modelBuilder.Entity<Article>().ToTable("Articles");
            modelBuilder.Entity<Tour>().ToTable("Tours");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Continent>().ToTable("Continents");
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<State>().ToTable("States");
            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<Currency>().ToTable("Currencies");
            modelBuilder.Entity<Image>().ToTable("Images");
            modelBuilder.Entity<Language>().ToTable("Languages");
            modelBuilder.Entity<TourCollection>().ToTable("TourCollections");
            modelBuilder.Entity<ArticleCollection>().ToTable("ArticleCollections");

            modelBuilder.Entity<TourRating>().ToTable("TourRatings");
            modelBuilder.Entity<TourReview>().ToTable("TourReviews");

            modelBuilder.Entity<RoleAccess>().Property(ra => ra.Action)
                .IsUnicode(false).HasMaxLength(70).IsRequired();
            modelBuilder.Entity<RoleAccess>().Property(ra => ra.Controller)
                .IsUnicode(false).HasMaxLength(70).IsRequired();
            modelBuilder.Entity<RoleAccess>().HasRequired(ra => ra.Role)
                .WithMany(r => r.RoleAccesses)
                .HasForeignKey(ra => ra.RoleId);

        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);


        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}