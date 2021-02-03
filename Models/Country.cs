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
    public class Country
    {
        [Key]
        public int CountryId { get; set; }       
        public string CountryName { get; set; }
        public string Code { get; set; }
        public string Language { get; set; }

        //foreign key continent table
        public int ContinentId { get; set; }
        public virtual Continent Continent { get; set; }
    }
}
