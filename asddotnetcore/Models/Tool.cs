using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
    public class Tool
    {
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
    }
}
