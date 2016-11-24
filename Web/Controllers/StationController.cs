using BL.DTOs.Filters;
using BL.DTOs.Stations;
using BL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class StationController : Controller
    {
        public RouteFacade RouteFacade { get; set; }

        public ActionResult Index(StationFilter filter = null)
        {
            return View(RouteFacade.GetAllStationsByFilter(filter));
        }

        public ActionResult CreateStation()
        {
            return View(new StationDTO());
        }

        [HttpPost]
        public ActionResult CreateStation(StationDTO station)
        {
            RouteFacade.CreateStation(station);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteStation(int id)
        {
            RouteFacade.DeleteStation(id);
            return RedirectToAction("Index");
        }
    }
}