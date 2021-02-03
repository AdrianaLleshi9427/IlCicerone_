using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IlCicerone.Models
{
     public partial class TourCollection
        {
            [Key]
            public int TourCollectionID { get; set; }
            //foreign key user table
            public string UserId { get; set; }

            [ForeignKey("UserId")]
            public ApplicationUser ApplicationUser { get; set; }
            public string TourCollectionTitle { get; set; }
            public string TourCollectionDetails { get; set; }
            [DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
                ApplyFormatInEditMode = true)]
            public DateTime CreatedCol_at { get; set; }
            public System.DateTime TourCollectionDate { get; set; }
            [DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
                ApplyFormatInEditMode = true)]
            public DateTime? EndCol_date { get; set; }
            [DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
                ApplyFormatInEditMode = true)]
            public DateTime? UpdatedCol_at { get; set; }
            public string TourColOwner { get; set; }
            public string CollectionImage { get; set; }
    }
    }
