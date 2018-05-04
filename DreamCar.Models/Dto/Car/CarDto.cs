using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamCar.Models.Dto.Dealer;

namespace DreamCar.Models.Dto.Car
{
    public class CarDto
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2), MaxLength(100)]
        public string Model { get; set; }
        
        [Required]
        [MinLength(5), MaxLength(10000)]
        public string Description { get; set; }
        
        [Range(0, short.MaxValue)]
        [Display(Name = "Year of biuld")]
        public short YearBuilt { get; set; }

        [Range(0, short.MaxValue)]
        [Display(Name = "Horse power")]
        public short HorsePower { get; set; }

        [Display(Name = "Dealer")]
        public string DealerName { get; set; }

        [Display(Name = "Dealer")]
        public int DealerId { get; set; }
    }
}
