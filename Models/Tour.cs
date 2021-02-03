using System;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Data;

namespace IlCicerone.Models
{
    public class Tour
    {
        [Key]
        public int TourId { get; set; }

        //foreign key user table
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        //foreign key category table
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        //foreign key city table
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public string TourName { get; set; }
        public string TourImage { get; set; }
        public string Description { get; set; }
        public string Story { get; set; }
        public string ArtandCulture { get; set; }

        public string EntertainmentandRecreation { get; set; }

        public string Whattovisitandexcursions { get; set; }
        public string Gastronomy { get; set; }

        public string Howandwhentoarrive { get; set; }

        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public double Price { get; set; }
        public string ActivityLevel { get; set; }
        public string Languages { get; set; }

        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
            ApplyFormatInEditMode = true)]
        public DateTime ? Publication_date { get; set; }

        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
            ApplyFormatInEditMode = true)]
        public DateTime Create_date { get; set; }

        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
            ApplyFormatInEditMode = true)]
        public DateTime ? End_date { get; set; }

        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
            ApplyFormatInEditMode = true)]
        public DateTime ? Updated_at { get; set; }
        public string Status { get; set; }
        public string Rating { get; set; }
        public int TotalRaters { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<TourReview> Reviews { get; set; }
        public virtual ICollection<Partecipant> Partecipants { get; set; }
        public int TourColId { get; set; }
        public virtual TourCollection TourCollection { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public string TourOwner { get; set; }
        public string ParentTourId { get; set; }

    }
}
