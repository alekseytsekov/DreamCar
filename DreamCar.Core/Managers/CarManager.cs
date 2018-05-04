using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamCar.Data.Repositories;
using DreamCar.Models.Common;
using DreamCar.Models.Domain;
using DreamCar.Models.Dto.Car;
using DreamCar.Models.Dto.Dealer;

namespace DreamCar.Core.Managers
{
    public class CarManager : ICarManager
    {
        private IRepository<Car> carRepo;
        
        public CarManager(IRepository<Car> carRepo)
        {
            this.carRepo = carRepo;
        }

        public (bool, string) AddCar(CarDto dto)
        {
            var entity = this.MapDtoToEntity(dto);
            if (entity == null)
            {
                return (false, "Invalid model!");
            }

            try
            {
                carRepo.Add(entity);
            }
            catch (Exception e)
            {
                // log local or in DB /e.Message/
                return (false, "Something went wrong! Cannot add car!");
            }

            return (true, $"Successfully add {dto.Model} car.");
        }

        public IEnumerable<CarDto> GetCars(CarListFilter filter, Paginator paginator)
        {
            var query = this.carRepo.AllAsQueryable();
            query = this.CreateQuery(query, filter);

            var entities = query
                            .OrderBy(x => x.YearBuilt)
                            .Skip(paginator.Skip())
                            .Take(paginator.Take)
                            .ToArray();

            var dtos = this.MapEntityToDto(entities);

            return dtos;
        }
        
        public int QueryCount(CarListFilter filter)
        {
            if (filter == null)
            {
                return this.carRepo.AllAsQueryable().Count();
            }

            var count = this.CreateQuery(carRepo.AllAsQueryable(), filter).Count();

            return count;
        }

        private IQueryable<Car> CreateQuery(IQueryable<Car> query, CarListFilter filter)
        {
            this.ProcessFilter(filter);

            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                query = query.Where(x => x.Description.ToLower().Contains(filter.Description));
            }

            if (filter.DealerId > 0)
            {
                query = query.Where(x => x.DealerId == filter.DealerId);
            }

            return query;
        }
        
        private void ProcessFilter(CarListFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                filter.Description = filter.Description.ToLower();
            }
        }

        private Car MapDtoToEntity(CarDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            var entity = new Car()
            {
                DealerId = dto.DealerId,
                Description = dto.Description,
                HorsePower = dto.HorsePower,
                Model = dto.Model,
                YearBuilt = dto.YearBuilt
            };

            return entity;
        }

        private CarDto MapEntityToDto(Car entity)
        {
            if (entity == null)
            {
                return null;
            }

            var dto = new CarDto()
            {
                Id = entity.Id,
                Description = entity.Description,
                HorsePower = entity.HorsePower,
                Model = entity.Model,
                YearBuilt = entity.YearBuilt,
                DealerId = entity.DealerId,
                DealerName = entity.Dealer?.Name
            };

            return dto;
        }

        private IEnumerable<CarDto> MapEntityToDto(IEnumerable<Car> entities)
        {
            if (entities == null || !entities.Any())
            {
                return Enumerable.Empty<CarDto>();
            }

            var result = new List<CarDto>();
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
