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
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        //foreign key category table
        public string CategoryName { get; set; }

        //foreign key user table
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }


    }
}
