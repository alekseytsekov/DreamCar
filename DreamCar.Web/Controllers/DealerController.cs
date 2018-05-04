
using System.Web.Mvc;
using DreamCar.Constants;
using DreamCar.Constants.Extensions;
using DreamCar.Core.Managers;
using DreamCar.Models.Common;
using DreamCar.Models.Dto.Dealer;

namespace DreamCar.Web.Controllers
{
    public class DealerController : BaseController
    {
        private IDealerManager dealerManager;
        public DealerController(IDealerManager dealerManager)
        {
            this.dealerManager = dealerManager;
        }

        [HttpGet]
        public ActionResult Add()
        {
            var dto = this.GetModelFromTempData<DealerDto>(TDKey.DealerDto);
            if(dto == null)
            {
                dto = new DealerDto();
            }

            return this.View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(DealerDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = this.GetModelStateError(ModelState);
                this.ShowErrorMessage(errors);

                this.AddModelToTempData(TDKey.DealerDto, dto);
                return this.RedirectToAction(nameof(this.Add).RemoveControllerSuffix());
            }
            
            bool isSuccess = false;
            string message = string.Empty;

            (isSuccess, message) = dealerManager.AddDealer(dto);
            if (!isSuccess)
            {
                this.ShowErrorMessage(message);

                this.AddModelToTempData(TDKey.DealerDto, dto);
                return this.RedirectToAction(nameof(this.Add).RemoveControllerSuffix());
            }

            var filter = this.GetModelFromTempData<DealerListFilter>(TDKey.DealerFilter);
            if (filter != null)
            {
                filter.Name = dto.Name;
                this.AddModelToTempData(TDKey.DealerFilter, filter);
            }

            this.ShowSuccessMessage(message);
            return this.RedirectToAction(nameof(this.ListDealers).RemoveControllerSuffix());
        }

        public ActionResult ListDealers()
        {
            var filter = this.GetModelFromTempData<DealerListFilter>(TDKey.DealerFilter);
            if (filter == null)
            {
                filter = new DealerListFilter();
            }

            this.AddModelToTempData(TDKey.DealerFilter, filter);

            var paginator = this.GetModelFromTempData<Paginator>(TDKey.Paginator);
            if (paginator == null)
            {
                paginator = new Paginator();
                paginator.ActionName = nameof(this.SetPage);
            }

            this.AddModelToTempData(TDKey.Paginator, paginator);

            int queryCount = this.dealerManager.QueryCount(filter);
            paginator.SetTotals(queryCount);

            var dtos = this.dealerManager.GetDealers(filter, paginator);

            var wrapper = new DealerWrapper();
            wrapper.Filter = filter;
            if (wrapper.Filter == null)
            {
                wrapper.Filter = new DealerListFilter();
            }

            wrapper.Paginator = paginator;
            if (wrapper.Paginator == null)
            {
                wrapper.Paginator = new Paginator();
            }
            
            wrapper.Dealers.AddRange(dtos);

            return this.View(wrapper);
        }

        [HttpGet]
        public ActionResult SetPage(Paginator paginator)
        {
            this.AddModelToTempData(TDKey.Paginator, paginator);

            var filter = this.GetModelFromTempData<DealerListFilter>(TDKey.DealerFilter);
            if (filter != null)
            {
                this.AddModelToTempData(TDKey.DealerFilter, filter);
            }

            return this.RedirectToAction(nameof(this.ListDealers));
        }

        [HttpGet]
        public ActionResult SetFilter(DealerListFilter filter)
        {
            this.AddModelToTempData(TDKey.DealerFilter, filter);

            var paginator = this.GetModelFromTempData<Paginator>(TDKey.Paginator);
            if (paginator != null)
            {
                this.AddModelToTempData(TDKey.Paginator, paginator);
            }

            return this.RedirectToAction(nameof(this.ListDealers));
        }
    }
}