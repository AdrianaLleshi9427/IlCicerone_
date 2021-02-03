using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlCicerone.Models
{
    public class Partecipant 
    {
        public int PartecipantId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [Display(Name = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Created By")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        [Display(Name = "Created By")]
        public ApplicationUser ApplicationUser { get; set; }
        public int TourId { get; set; }
        public bool Confirmed { get; set; }
        public string Address { get; set; }
    }
}