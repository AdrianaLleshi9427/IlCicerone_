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
    public class State
    {
        [Key]
        public int StateId { get; set; }        
        public string StateName { get; set; }

        //foreign key country table
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
