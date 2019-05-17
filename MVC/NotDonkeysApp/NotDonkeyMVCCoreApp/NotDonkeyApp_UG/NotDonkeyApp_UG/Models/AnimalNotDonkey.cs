using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotDonkeyApp_UG.Models
{
    public class AnimalNotDonkey
    {
        public int Id { get; set; }

        [Required]
        public int OwnerPhoneNumber { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Name length can't be more than 30.")]
        public string Name { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Password length can't be more than 30.")]
        public string Password { get; set; }

        [Required]
        public string AnimalType { get; set; } // also name of avatar

        [Required]
        [StringLength(15)]
        public string Sex { get; set; }

        [Required]
        [Range(0, 2137, ErrorMessage = "Oops")]
        public int Weight { get; set; }

        [Required]
        [StringLength(2137, ErrorMessage = "Additional Description may contain up to 2137 letters")]
        public string AdditionalDescription { get; set; }

        [Required]
        public bool IsDonkey { get; set; }

        public string AnimalsYouLike { get; set; }
    }
}
