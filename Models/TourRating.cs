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
        public class TourRating
        {
            [Key]
            public int CommentId { get; set; }
            public string Comments { get; set; }
            public DateTime ThisDateTime { get; set; }

            //[Key]
            //[ForeignKey("FK_dbo.TourRating_dbo.Tour_TourId")]
            public int TourId { get; set; }
            public virtual Tour Tour { get; set; } 
            public int Rating { get; set; }
    }
}
