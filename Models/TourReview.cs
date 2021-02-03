using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlCicerone.Models
{
    public class TourReview 
    {
        public int Id { get; set; }
        [Range(1, 10)]
        [Required]
        public int Rating { get; set; }
        [Required]
        [StringLength(1024)]
        public string Body { get; set; }
        public int TourId { get; set; }
        [Display(Name = "User Name")]
        [DisplayFormat(NullDisplayText = "anonymous")]
        public string ReviewerName { get; set; }
        [Display(Name = "Visited On")]
        [DataType(DataType.Date)]
        public DateTime VisitedOn { get; set; }
    }
}