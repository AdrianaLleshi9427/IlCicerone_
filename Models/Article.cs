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
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

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
        public int ArticleColId { get; set; }
        public virtual ArticleCollection ArticleCollection { get; set; }
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        public string Title { get; set; }
        public string Gallery { get; set; }
        public string Description { get; set; }
        public string ArticleImage { get; set; }

        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
            ApplyFormatInEditMode = true)]
        public DateTime Created_at { get; set; }
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = @"{0:dd\/MM\/yyyy HH:mm}",
            ApplyFormatInEditMode = true)]
        public DateTime ? Updated_at { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ? Publication_date { get; set; }
        public string ArticleBody { get; set; }
        public string ArticleOwner { get; set; }
        public 
        string Status { get; set; }
        public int ParentArticleId { get; set; } 

    }


}
