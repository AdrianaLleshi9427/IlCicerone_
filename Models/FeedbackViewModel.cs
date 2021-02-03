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
    public class FeedbackViewModel
    {
        public string Comment { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int? Select { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
