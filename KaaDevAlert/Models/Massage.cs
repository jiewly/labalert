using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KaaDevAlert.Models
{
    public class Massage
    {
        [Display(Name = "No.")]
        public int Id { get; set; }
        public string Lable { get; set; }
        public int Type { get; set; }
        public Configuration Configurationtype { get; set; }
    }
}
