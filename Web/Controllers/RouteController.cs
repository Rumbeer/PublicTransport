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
            var routes = RouteFacade.ListCompanyRoutes(companyId, page);
            var model = GetProductListModel(routes);

            model.HasTemplate = new Dictionary<int, bool>(); 
            foreach (var route in routes.ResultsPage)
            {
                if(RouteFacade.GetRouteStationsByRoute(route.ID).Count == 0)
                {
                    model.HasTemplate.Add(route.ID, false);
                }
                else
                {
                    model.HasTemplate.Add(route.ID, true);
                }
            }

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
                    RouteStation = new RouteStationDTO(),
                    TimeFromFirstStation = "00:00:00",
                    TimeToNextStation = "00:00:00"
                });
            }

            return View(templates);
        }

        [HttpPost]
        public ActionResult CreateRouteTemplate(List<CreateRouteTemplatesModel> model)
        {
            string format = "g";
            TimeSpan timeFromFirstStation, timeToNextStation;
            CultureInfo  culture = CultureInfo.CurrentCulture;
            var isOrderValid = new bool[model.Count + 1];
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            foreach (var template in model)
            {
                if(template.RouteStation.Order > 0 && template.RouteStation.Order < isOrderValid.Count())
                {
                    isOrderValid[template.RouteStation.Order] = true;
                }
                else
                {
                    ViewBag.Message = "Invalid order";
                    return View(model);
                }
            }
            for( int i = 1; i < isOrderValid.Count(); i++)
            {
                if (!isOrderValid[i])
                {
                    ViewBag.Message = "Invalid order";
                    return View(model);
                }
            }

            foreach (var template in model)
            {
                var station = RouteFacade.GetAllStationsByFilter(new StationFilter { Name = template.StationName, Town = template.StationTown });
                if(!station.Any())
                {
                    ViewBag.Message = "Station (Name: " + template.StationName + " Town: " + template.StationTown + ") not found";
                    return View(model);
                }          
                if(!(template.RouteStation.DistanceFromPreviousStation > 0))
                {
                    ViewBag.Message = "Station (Name: " + template.StationName + " Town: " + template.StationTown + ") distance < 0";
                    return View(model);
                }     

                if(TimeSpan.TryParseExact(template.TimeFromFirstStation, format, culture, out timeFromFirstStation) &&
                    TimeSpan.TryParseExact(template.TimeToNextStation, format, culture, out timeToNextStation))
                {
                    template.RouteStation.TimeFromFirstStation = timeFromFirstStation;
                    template.RouteStation.TimeToNextStation = timeToNextStation;
                }
                else
                {
                    ViewBag.Message = "Invalid time format";
                    return View(model);
                }
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

        private RouteListModel GetProductListModel(RouteListQueryResultDTO routes)
        {
            return new RouteListModel
            {
                CompanyId = routes.CompanyId,
                HasTemplate = new Dictionary<int, bool>(),
                Routes = new StaticPagedList<RouteDTO>(routes.ResultsPage, routes.RequestedPage, RouteFacade.RoutePageSize, routes.TotalResultCount)
            };
        } 
    }
}