using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamCar.Models.Common;
using DreamCar.Models.Dto.Dealer;

namespace DreamCar.Core.Managers
{
    public interface IDealerManager
    {
        (bool, string) AddDealer(DealerDto dto);
        IEnumerable<DealerDto> GetDealers(DealerListFilter filter, Paginator paginator);
        int QueryCount(DealerListFilter filter);
        bool IsDealerExist(int dealerId);
    }
}
