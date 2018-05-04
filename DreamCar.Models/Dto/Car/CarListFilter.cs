using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamCar.Models.Dto.Dealer;

namespace DreamCar.Models.Dto.Car
{
    public class CarListFilter
    {
        public CarListFilter()
        {
            this.Dealers = new List<DealerDto>();
        }
        
        public string Description { get; set; }

        public int DealerId { get; set; }

        public IList<DealerDto> Dealers { get; set; }
    }
}
