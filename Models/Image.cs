using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.IO;
using System.Data;

namespace IlCicerone.Models
{
    public partial class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display (Name="Image Title")]
        public string ImageTitle { get; set; }

        [Display(Name = "File Name")]
        public string OriginalFileName { get; set; }

        [Display(Name = "Image File")]
        public string ImagePath { get; set; }
        public virtual Tour TourId { get; set; }
    }
}