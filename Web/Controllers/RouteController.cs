using BL.DTOs.Filters;
using BL.DTOs.Routes;
using BL.DTOs.RouteStations;
using BL.Facades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Web.Models;
using X.PagedList;

namespace Web.Controllers
{
    public class RouteController : Controller
    {
        public RouteFacade RouteFacade { get; set; }

        // GET: Route
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CompanyRoutes(int companyId, int page = 1)
        {
            var result = RouteFacade.ListCompanyRoutes(companyId, page);

            var model = GetProductListModel(result);

            return View(model);
        }

        public ActionResult CreateRoute(int companyId)
        {
            return View(new CreateRouteModel
            {
                Route = new RouteDTO(),
                CompanyId = companyId
            });
        }

        [HttpPost]
        public ActionResult CreateRoute(CreateRouteModel model)
        {
            RouteFacade.CreateRoute(model.Route, model.CompanyId);
            return RedirectToAction("CompanyRoutes", new { companyId = model.CompanyId });
        }

        public ActionResult DeleteRoute(int routeId, int companyId)
        {
            RouteFacade.DeleteRoute(routeId);
            return RedirectToAction("CompanyRoutes", companyId);
        }

        public ActionResult GetStationCountForRouteTemplate(int companyId, int routeId)
        {
            return View(new int[] { companyId, routeId, 1} );
        }


        [HttpPost]
        public ActionResult GetStationCountForRouteTemplate(int[] values)
        {
            int companyId = values[0];
            int routeId = values[1];
            int stationCount = values[2];
            return RedirectToAction("CreateRouteTemplate", new { companyId, routeId, stationCount });
        }

        public ActionResult CreateRouteTemplate(int companyId, int routeId, int stationCount)
        {
            var templates = new List<CreateRouteTemplatesModel>();
            for (int i = 0; i < stationCount; i++)
            {
                templates.Add(new CreateRouteTemplatesModel
                {
                    CompanyId = companyId,
                    RouteId = routeId,
                    RouteStation = new RouteStationDTO()
                });
            }

            return View(templates);
        }

        [HttpPost]
        public ActionResult CreateRouteTemplate(List<CreateRouteTemplatesModel> model)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            foreach (var template in model)
            {
                var station = RouteFacade.GetAllStationsByFilter(new StationFilter { Name = template.StationName, Town = template.StationTown });
                template.RouteStation.TimeFromFirstStation = TimeSpan.Parse(template.TimeFromFirstStation);
                template.RouteStation.TimeToNextStation = TimeSpan.Parse(template.TimeToNextStation);
                RouteFacade.AddRouteStation(station.FirstOrDefault().ID, template.RouteId, template.RouteStation);
            }
            return RedirectToAction("CompanyRoutes", new { companyId = model.FirstOrDefault().CompanyId });
        }
        
//        public ActionResult Test()
//        {
//            var model = new List<TestModel>() {
//                new TestModel
//            {
//                List = null
//            } };
//            return View(model);
//        }
//
//        [HttpPost]
//        public ActionResult Test(List<TestModel> model)
//        {
//                model.Add(new TestModel { List = "ahoj" });
//                return View(model);
//            return RedirectToAction("Home", "Index", "Home");
//        }
//        
//        public ActionResult Test2(List<TestModel> model)
//        {
//
//            model.Add(new TestModel { List = "ahoj" });
//            return View(model);
//            return RedirectToAction("Home", "Index", "Home");
//        }

        private RouteListModel GetProductListModel(RouteListQueryResultDTO result)
        {
            return new RouteListModel
            {
                CompanyId = result.CompanyId,
                Routes = new StaticPagedList<RouteDTO>(result.ResultsPage, result.RequestedPage, RouteFacade.RoutePageSize, result.TotalResultCount)
            };
        } 
    }
}