using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Entities
{
    public class BookCopies
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int NumberOfCopies { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public int BookId { get; set; }
        [ForeignKey("LibraryId")]
        public Library Library { get; set; }
        public int LibraryId { get; set; }
    }
}
