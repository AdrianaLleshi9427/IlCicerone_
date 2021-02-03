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
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string CityName { get; set; }

        //foreign key state table
        public int StateId { get; set; }
        public virtual State State { get; set; }
    }
}
