using BL.DTOs.Filters;
using BL.DTOs.Stations;
using BL.Facades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

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

        public ActionResult SetImageOfStation(int id)
        {
            return View(new StationPhotoModel { Id = id });
        }

        [HttpPost]
        public ActionResult SetImageOfStation(int id, StationPhotoModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/")
                                                          + file.FileName);
                    model.ImagePath = file.FileName;
                }
                RouteFacade.SetImageOfStation(model.Id, model.ImagePath);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}