using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class PublisherForManipulationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
    }
}
