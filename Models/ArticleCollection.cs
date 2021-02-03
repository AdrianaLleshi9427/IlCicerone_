using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IlCicerone.Models
{
     public partial class ArticleCollection
        {
            [Key]
            public int ArticleCollectionID { get; set; }
            //foreign key user table
            public string UserId { get; set; }

            [ForeignKey("UserId")]
            public ApplicationUser ApplicationUser { get; set; }
            public string ArticleCollectionTitle { get; set; }
            public string ArticleCollectionDetails { get; set; }
            [DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
                ApplyFormatInEditMode = true)]
            public DateTime CreatedCol_at { get; set; }
            [DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
                ApplyFormatInEditMode = true)]
            public DateTime? UpdatedCol_at { get; set; }
            public System.DateTime ArticleCollectionDate { get; set; }
            public string ArticleColOwner { get; set; }
            public string CollectionImage { get; set; }
    }
    }
