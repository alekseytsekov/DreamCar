using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamCar.Models.Common;
using DreamCar.Models.Dto.Dealer;

namespace DreamCar.Models.Dto.Car
{
    public class CarWrapper
    {
        public CarWrapper()
        {
            this.Cars = new List<CarDto>();    
            this.Dealers = new List<DealerDto>();
        }

        public CarDto Car { get; set; }
        public CarListFilter Filter { get; set; }
        public Paginator Paginator { get; set; }
        public IList<CarDto> Cars { get; set; }
        public IList<DealerDto> Dealers { get; set; }
    }
}
