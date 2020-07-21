using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class ClientForManipulation
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
