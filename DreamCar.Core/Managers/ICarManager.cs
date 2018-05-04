using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamCar.Models.Common;
using DreamCar.Models.Dto.Car;

namespace DreamCar.Core.Managers
{
    public interface ICarManager
    {
        (bool, string) AddCar(CarDto dto);
        IEnumerable<CarDto> GetCars(CarListFilter filter, Paginator paginator);
        int QueryCount(CarListFilter filter);
    }
}
