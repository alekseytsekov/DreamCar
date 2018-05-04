using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using DreamCar.Data.Repositories;
using DreamCar.Models.Common;
using DreamCar.Models.Domain;
using DreamCar.Models.Dto.Dealer;

namespace DreamCar.Core.Managers
{
    public class DealerManager : IDealerManager
    {
        private IRepository<Dealer> dealerRepo;

        public DealerManager(IRepository<Dealer> dealerRepo)
        {
            this.dealerRepo = dealerRepo;
        }

        public (bool, string) AddDealer(DealerDto dto)
        {
            var entity = this.MapDtoToEntity(dto);
            if (entity == null)
            {
                return (false, "Invalid model!");
            }

            try
            {
                dealerRepo.Add(entity);
            }
            catch (Exception e)
            {
                // log local or in DB /e.Message/
                return (false, "Something went wrong! Cannot add dealer!");
            }

            return (true, $"Successfully add {dto.Name} dealer.");
        }

        public IEnumerable<DealerDto> GetDealers(DealerListFilter filter, Paginator paginator)
        {
            var query = this.dealerRepo.AllAsQueryable();
            Dealer[] entities = null;

            if (filter != null && paginator != null)
            {
                query = this.CreateQuery(query, filter);

                entities = query
                    .OrderBy(x => x.Name)
                    .Skip(paginator.Skip())
                    .Take(paginator.Take)
                    .ToArray();
            }
            else
            {
                entities = query.OrderBy(x => x.Name).ToArray();
            }
            
            var dtos = this.MapEntityToDto(entities);

            return dtos;
        }

        public bool IsDealerExist(int dealerId)
        {
            return this.dealerRepo.Any(x => x.Id == dealerId);
        }

        private void ProcessFilter(DealerListFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                filter.Name = filter.Name.ToLower();
            }
        }

        public int QueryCount(DealerListFilter filter)
        {
            if (filter == null)
            {
                return this.dealerRepo.AllAsQueryable().Count();
            }

            var count = this.CreateQuery(dealerRepo.AllAsQueryable(), filter).Count();

            return count;
        }

        private IQueryable<Dealer> CreateQuery(IQueryable<Dealer> query, DealerListFilter filter)
        {
            this.ProcessFilter(filter);
            
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(x => x.Name.ToLower().Contains(filter.Name)); 
            }
            
            return query;
        }


        private Dealer MapDtoToEntity(DealerDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            var entity = new Dealer()
            {
                Name = dto.Name
            };

            return entity;
        }

        private DealerDto MapEntityToDto(Dealer entity)
        {
            if (entity == null)
            {
                return null;
            }

            var dto = new DealerDto()
            {
                Id = entity.Id,
                Name = entity.Name
            };

            return dto;
        }

        private IEnumerable<DealerDto> MapEntityToDto(IEnumerable<Dealer> entities)
        {
            if (entities == null || !entities.Any())
            {
                return Enumerable.Empty<DealerDto>();
            }

            var result = new List<DealerDto>();
            foreach (var entity in entities)
            {
                var dto = this.MapEntityToDto(entity);
                if (dto != null)
                {
                    result.Add(dto);
                }
            }

            return result;
        }
    }
}
