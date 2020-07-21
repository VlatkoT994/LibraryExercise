using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class BookCopiesForCreationDto
    {
        public int NumberOfCopies { get; set; }
        public int BookId { get; set; }
    }
}
