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

        // GET: SpecificRoute
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateSpecificRoute(CreateSpecificRouteModel model)
        {
            try
            {
                RouteFacade.CreateSpecificRoute(model.RouteId, model.DepartTime, model.VehicleId);
            }
            catch(ArgumentNullException e)
            {
                return null;
            }
            return null;
        }
    }
}