using BL.DTOs.Filters;
using BL.DTOs.Vehicles;
using BL.Facades;
using System;
using System.Web.Mvc;
using Web.Models;
using X.PagedList;

namespace Web.Controllers
{
    public class VehicleController : Controller
    {
        public CompanyFacade CompanyFacade { get; set; }

        public ActionResult CompanyVehicles(int companyId, int page = 1)
        {
            var result = CompanyFacade.ListVehicles(new VehicleFilter
            {
                CompanyId = companyId
            }, page);

            var model = GetVehicleListModel(result);

            return View(model);
        }

        public ActionResult CreateVehicle(int companyId)
        {
            return View(new CreateVehicleModel
            {
                Vehicle = new VehicleDTO(),
                CompanyId = companyId
            });
        }

        [HttpPost]
        public ActionResult CreateVehicle(CreateVehicleModel model)
        {
            try
            {
                CompanyFacade.CreateVehicle(model.Vehicle, model.CompanyId);
            }
            catch (ArgumentException e)
            {
                ViewBag.Message = e.Message;
                return View(model);
            }
            return RedirectToAction("CompanyVehicles", new { companyId = model.CompanyId });
        }

        public ActionResult DeleteVehicle(int vehicleId, int companyId)
        {
            CompanyFacade.DeleteVehicle(vehicleId);
            return RedirectToAction("CompanyVehicles", new { companyId = companyId });
        }


        private VehicleListModel GetVehicleListModel(VehicleListQueryResultDTO result)
        {
            return new VehicleListModel
            {
                CompanyId = result.Filter.CompanyId.GetValueOrDefault(),
                Vehicles = new StaticPagedList<VehicleDTO>(result.ResultsPage, result.RequestedPage, CompanyFacade.VehiclePageSize, result.TotalResultCount)
            };
        }
    }
}