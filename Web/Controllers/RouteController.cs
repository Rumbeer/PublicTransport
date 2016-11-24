using BL.DTOs.Routes;
using BL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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