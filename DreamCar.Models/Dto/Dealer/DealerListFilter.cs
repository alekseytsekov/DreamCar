using System.ComponentModel.DataAnnotations;

namespace DreamCar.Models.Dto.Dealer
{
    public class DealerListFilter
    {
        [MinLength(3),MaxLength(20)]
        public string Name { get; set; }
    }
}
