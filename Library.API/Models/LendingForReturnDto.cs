using Library.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public class LendingForReturnDto
    {
        public int Id { get; set; }
        public DateTime? WitdrawDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public BookForReturnDto Book { get; set; }
    }
}
