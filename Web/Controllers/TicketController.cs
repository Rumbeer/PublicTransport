using BL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class TicketController : Controller
    {
        public RouteFacade RouteFacade { get; set; }

        public ActionResult OrderTicket(int departRouteStationId, int arrivalRouteStationId)
        {
            return View();
        }
    }
}