using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class LendingForCreateDto
    {
        public DateTime? WitdrawDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        [Required]
        public int BookId { get; set; }
    }
}
