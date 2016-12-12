using BL.DTOs.Programs;
using BL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class TicketController : Controller
    {
        public RouteFacade RouteFacade { get; set; }

        public ActionResult EmptySeats(int departRouteStationId, int arrivalRouteStationId)
        {
            var emptyPrograms = RouteFacade.ListEmptyProgramsOfRouteStations(
                RouteFacade.GetRouteStationsBetween(departRouteStationId, arrivalRouteStationId));
            var model = new EmptySeatsModel
            {
                Programs = new Dictionary<int, List<ProgramDTO>>()
            };
            foreach(var programs in emptyPrograms)
            {
                model.Programs.Add(RouteFacade.GetSeatNumberFromProgram(programs.First().ID), programs);
            }
            TempData["model"] = model;
            return View(model);
        }
        public ActionResult Recap(int seatNumber)
        {
            var model = (EmptySeatsModel)TempData["model"];
            return View();
        }
    }
}