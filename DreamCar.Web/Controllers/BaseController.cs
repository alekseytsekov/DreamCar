using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DreamCar.Constants;
using Newtonsoft.Json;

namespace DreamCar.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public void ShowSuccessMessage(string message)
        {
            this.TempData[TDKey.SuccessMsg] = message;
        }

        public void ShowInfoMessage(string message)
        {
            this.TempData[TDKey.InfoMsg] = message;
        }

        public void ShowWarningMessage(string message)
        {
            this.TempData[TDKey.WarningMsg] = message;
        }

        public void ShowErrorMessage(string message)
        {
            this.TempData[TDKey.ErrorMsg] = message;
        }

        protected T GetModelFromTempData<T>(string key)
        {
            T dto = default(T);

            if (this.TempData.ContainsKey(key) &&
                this.TempData[key] != null)
            {
                dto = JsonConvert.DeserializeObject<T>(
                    this.TempData[key] as string);

                this.TempData[key] = null;
            }

            return dto;
        }

        protected void AddModelToTempData<T>(string key, T dto)
        {
            this.TempData[key] = null;
            this.TempData[key] = JsonConvert.SerializeObject(dto);
        }

        protected string GetModelStateError(ModelStateDictionary modelStateDict)
        {
            var sb = new StringBuilder();

            foreach (var modelStateErr in modelStateDict.Values)
            {
                foreach (var error in modelStateErr.Errors)
                {
                    sb.Append(error.ErrorMessage + " ");
                }
            }

            return sb.ToString().Trim();
        }
    }
}