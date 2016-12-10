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

        // GET: SpecificRoute
        public ActionResult RouteDetails(int routeId, int companyId)
        {
            var model = new RouteDetailsModel();
            model.DepartTimes = RouteFacade.GetRouteDepartTimes(routeId);
            model.Templates = RouteFacade.GetRouteTemplate(routeId);
            model.CompanyId = companyId;
            model.RouteId = routeId;
            return View(model);
        }

        public ActionResult CreateSpecificRoute(int routeId, int companyId)
        {
            var model = new CreateSpecificRouteModel
            {
                RouteId = routeId,
                CompanyId = companyId
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateSpecificRoute(CreateSpecificRouteModel model)
        {
            DateTime departTime;
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