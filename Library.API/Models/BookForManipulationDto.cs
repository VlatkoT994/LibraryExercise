using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class BookForManipulationDto
    {
        public string Title { get; set; }
        public DateTime YearOfIssue { get; set; }
        public int NumberOfPages { get; set; }
    }
}
