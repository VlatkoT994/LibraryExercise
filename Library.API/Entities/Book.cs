using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime YearOfIssue { get; set; }
        public int NumberOfPages { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
        public int PublisherId { get; set; }
        public IEnumerable<Lending> Lendings { get; set; }
            = new List<Lending>();
    }
}
