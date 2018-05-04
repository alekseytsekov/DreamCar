using System.ComponentModel.DataAnnotations;

namespace DreamCar.Models.Dto.Dealer
{
    public class DealerDto
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }
    }
}
