
using System;
using System.Linq;
using System.Web.Mvc;
using DreamCar.Constants;
using DreamCar.Constants.Extensions;
using DreamCar.Core.Managers;
using DreamCar.Models.Common;
using DreamCar.Models.Dto.Car;

namespace DreamCar.Web.Controllers
{
    public class CarController : BaseController
    {
        private ICarManager carManager;
        private IDealerManager dealerManager;

        public CarController(ICarManager carManager, IDealerManager dealerManager)
        {
            this.carManager = carManager;
            this.dealerManager = dealerManager;
        }

        [HttpGet]
        public ActionResult Add()
        {
            var wrapper = this.GetModelFromTempData<CarWrapper>(TDKey.CarWrapper);
            if (wrapper == null)
            {
                wrapper = new CarWrapper();
                wrapper.Car = new CarDto();
            }

            // populate dealers
            var dtoDealers = this.dealerManager.GetDealers(null, null);
            wrapper.Dealers.AddRange(dtoDealers);

            return this.View(wrapper);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CarWrapper wrapper)
        {
            if (!ModelState.IsValid)
            {
                var errors = this.GetModelStateError(ModelState);
                this.ShowErrorMessage(errors);

                this.AddModelToTempData(TDKey.CarWrapper, wrapper);
                return this.RedirectToAction(nameof(this.Add).RemoveControllerSuffix());
            }

            if (wrapper.Car.YearBuilt > DateTime.UtcNow.Year)
            {
                this.ShowErrorMessage("Year built cannot be in a future!");
                this.AddModelToTempData(TDKey.CarWrapper, wrapper);
                return this.RedirectToAction(nameof(this.Add).RemoveControllerSuffix());
            }

            var isDealerExist = this.dealerManager.IsDealerExist(wrapper.Car.DealerId);
            if (!isDealerExist)
            {
                this.ShowErrorMessage("Dealer does not exist! Choose valid Dealer!");
                this.AddModelToTempData(TDKey.CarWrapper, wrapper);
                return this.RedirectToAction(nameof(this.Add).RemoveControllerSuffix());
            }

            bool isSuccess = false;
            string message = string.Empty;

            (isSuccess, message) = carManager.AddCar(wrapper.Car);
            if (!isSuccess)
            {
                this.ShowErrorMessage(message);

                this.AddModelToTempData(TDKey.CarWrapper, wrapper);
                return this.RedirectToAction(nameof(this.Add).RemoveControllerSuffix());
            }

            var filter = this.GetModelFromTempData<CarListFilter>(TDKey.CarListFilter);
            if (filter != null)
            {
                filter.DealerId = wrapper.Car.DealerId;
                this.AddModelToTempData(TDKey.CarListFilter, filter);
            }

            this.ShowSuccessMessage(message);
            return this.RedirectToAction(nameof(this.ListCars).RemoveControllerSuffix());
        }

        public ActionResult ListCars()
        {
            var filter = this.GetModelFromTempData<CarListFilter>(TDKey.CarListFilter);
            if(filter == null)
            {
                filter = new CarListFilter();
            }

            if (!filter.Dealers.Any())
            {
                var dealers = this.dealerManager.GetDealers(null, null);
                filter.Dealers.AddRange(dealers);
            }
            
            this.AddModelToTempData(TDKey.CarListFilter, filter);

            var paginator = this.GetModelFromTempData<Paginator>(TDKey.Paginator);
            if (paginator == null)
            {
                paginator = new Paginator();
                //paginator.ActionName = nameof(CarController).RemoveControllerSuffix() + "/" +  nameof(this.SetPage);
                paginator.ActionName = nameof(this.SetPage);
            }

            this.AddModelToTempData(TDKey.Paginator, paginator);

            int queryCount = this.carManager.QueryCount(filter);
            paginator.SetTotals(queryCount);

            var dtos = this.carManager.GetCars(filter, paginator);

            var wrapper = new CarWrapper();
            wrapper.Filter = filter;
            if (wrapper.Filter == null)
            {
                wrapper.Filter = new CarListFilter();
            }

            wrapper.Paginator = paginator;
            if (wrapper.Paginator == null)
            {
                wrapper.Paginator = new Paginator();
            }

            wrapper.Cars.AddRange(dtos);

            return this.View(wrapper);
        }

        [HttpGet]
        public ActionResult SetPage(Paginator paginator)
        {
            this.AddModelToTempData(TDKey.Paginator, paginator);

            var filter = this.GetModelFromTempData<CarListFilter>(TDKey.CarListFilter);
            if (filter != null)
            {
                this.AddModelToTempData(TDKey.CarListFilter, filter);
            }

            return this.RedirectToAction(nameof(this.ListCars));
        }

        [HttpGet]
        public ActionResult SetFilter(CarListFilter filter)
        {
            if (!string.IsNullOrWhiteSpace(filter.Description) && (filter.Description.Length < 3 || filter.Description.Length > 20))
            {
                filter.Description = string.Empty;
            }

            this.AddModelToTempData(TDKey.CarListFilter, filter);

            var paginator = this.GetModelFromTempData<Paginator>(TDKey.Paginator);
            if (paginator != null)
            {
                this.AddModelToTempData(TDKey.Paginator, paginator);
            }

            return this.RedirectToAction(nameof(this.ListCars));
        }
    }
}