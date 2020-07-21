using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Entities
{
    public class Lending
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime WitdrawDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public int BookId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public int ClientId { get; set; }

    }
}
