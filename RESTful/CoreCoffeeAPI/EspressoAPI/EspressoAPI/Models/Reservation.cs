using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EspressoAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/")]
        public string Phone { get; set; }

        [Required]
        public int TotalPeople { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public string Time { get; set; }
    }
}
