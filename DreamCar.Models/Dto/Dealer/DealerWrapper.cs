using System.Collections.Generic;
using DreamCar.Models.Common;

namespace DreamCar.Models.Dto.Dealer
{
    public class DealerWrapper
    {
        public DealerWrapper()
        {
            this.Dealers = new List<DealerDto>();
        }

        public DealerDto Dealer { get; set; }

        public DealerListFilter Filter { get; set; }

        public Paginator Paginator { get; set; }

        public IList<DealerDto> Dealers { get; set; }
    }
}
