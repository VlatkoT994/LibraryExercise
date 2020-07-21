using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class BookCopiesForReturnDto
    {
        public int Id { get; set; }
 
        public int NumberOfCopies { get; set; }
        public BookForReturnDto Book { get; set; }
        public LibraryForReturnDto Library { get; set; }
        public int LibraryId { get; set; }
    }
}
