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
    public class Continent
    {
        [Key]
        public int ContinentId { get; set; }
        public string ContinentName { get; set; }
        public string Code { get; set; }
    }
}
