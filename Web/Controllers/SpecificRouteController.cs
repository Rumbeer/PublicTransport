using BL.DTOs.Filters;
using BL.DTOs.Stations;
using BL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class SpecificRouteController : Controller
    {
        public RouteFacade RouteFacade { get; set; } 
        public CompanyFacade CompanyFacade { get; set; }

        public ActionResult Search()
        {
            var model = new SearchRouteModel()
            {
                Time = DateTime.Now
            };
            return View(model);
        }

        // GET: SpecificRoute
        public ActionResult RouteDetails(int routeId, int companyId)
        {
            var model = new RouteDetailsModel();
            model.DepartTimes = RouteFacade.GetRouteDepartTimes(routeId);
            model.Templates = RouteFacade.GetRouteTemplate(routeId);
            model.Stations = new Dictionary<int, StationDTO>();
            foreach(var template in model.Templates)
            {
                model.Stations.Add(template.Order, RouteFacade.GetStationByRouteStation(template.ID));
            }
            model.CompanyId = companyId;
            model.RouteId = routeId;
            return View(model);
        }

        [HttpPost]
        public ActionResult Search(SearchRouteModel model)
        {
            var departStation = RouteFacade.GetAllStationsByFilter(new StationFilter
            {
                Name = model.DepartName,
                Town = model.DepartTown
            }).FirstOrDefault();
            var arriveStation = RouteFacade.GetAllStationsByFilter(new StationFilter
            {
                Name = model.ArriveName,
                Town = model.ArriveTown
            }).FirstOrDefault();
            if(arriveStation == null || departStation == null || !ModelState.IsValid)
            {
                ViewBag.Message = "Station cannot be null";
                return View(model);
            }
            var routeStations = RouteFacade.FindRoutesWithStations(departStation.ID, arriveStation.ID, model.Time);
            var resultModel = new SearchResultsModel
            {
                DepartStation = departStation,
                ArriveStation = arriveStation,
                Results = routeStations
            };
            //return View("DisplaySearchResults", resultModel);
            TempData["model"] = resultModel;
            return RedirectToAction("DisplaySearchResults");
        }

        public ActionResult DisplaySearchResults(SearchResultsModel model)
        {
            model = (SearchResultsModel)TempData["model"];
            return View(model);
        }

        public ActionResult CreateSpecificRoute(int routeId, int companyId)
        {
            var list = CompanyFacade.GetVehicleLicencePlates(companyId).ToList();
            var model = new CreateSpecificRouteModel
            {
                RouteId = routeId,
                CompanyId = companyId,
                LicencePlates = list
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateSpecificRoute(CreateSpecificRouteModel model)
        {
            DateTime departTime;
            model.VehicleId = CompanyFacade.GetVehicleIdByLicencePlate(model.LicencePlate);
            if(!DateTime.TryParse(model.DepartTime, out departTime) || !ModelState.IsValid)
            {
                ViewBag.Message = "Incorrect depart time format";
                return View(model);
            }
            var vehicle = CompanyFacade.GetVehicleById(model.VehicleId, model.CompanyId);
            if(vehicle == null)
            {
                ViewBag.Message = "Vehicle with this id does not exist or belongs to other company";
                return View(model);
            }
            RouteFacade.CreateSpecificRoute(model.RouteId, departTime, model.VehicleId);

            return RedirectToAction("RouteDetails", new { routeId = model.RouteId, companyId = model.CompanyId });
        }
    }
}