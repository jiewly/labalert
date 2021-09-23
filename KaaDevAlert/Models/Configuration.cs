using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KaaDevAlert.Models
{
    public class Configuration
    {
        [Display(Name = "No.")]
        public int Id { get; set; }
        public string KeyNumber { get; set; }
        public string Value { get; set; }
     
    }
}
