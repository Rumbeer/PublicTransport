using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BL.Facades;
using BL.DTOs.Routes;
using System.Diagnostics;
using BL.DTOs.RouteStations;
using API.Models;

namespace API.Controllers
{
    public class LinkController : ApiController
    {
        public RouteFacade RouteFacade { get; set; }

        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.OK, RouteFacade.ListAllRoutes());
        }

        public IHttpActionResult Get(int id)
        {
            const int order = 1; //constant for routeStation order
            List<RouteStationDTO> routeStations = RouteFacade.GetRouteStationsByRoute(id).OrderBy(key => key.Order).ToList();
            List<RouteStationDTO> firstRouteStations = routeStations.Where(rStations => rStations.Order == order).ToList();
            List<RouteStationDTO> lastRouteStations = routeStations.Where(rStations => rStations.Order == routeStations.Select(key => key.Order).Last()).ToList();
            List<SpecificLinkModel> linkModels = new List<SpecificLinkModel>();
            foreach (var firstRouteStation in firstRouteStations)
            {
                SpecificLinkModel linkModel = new SpecificLinkModel();
                linkModel.RouteId = id;
                linkModel.DepartFromFirstStation = firstRouteStation.DepartFromFirstStation;
                linkModel.NameOfFirstStation = RouteFacade.GetStationNameByRouteStation(firstRouteStation.ID);
                var specificLastStation = lastRouteStations.Where(rStation => rStation.TimeFromFirstStation == firstRouteStation.TimeFromFirstStation).FirstOrDefault();
                if(specificLastStation == null)
                {
                    break;
                }
                linkModel.NameOfLastStation = RouteFacade.GetStationNameByRouteStation(specificLastStation.ID);
                linkModels.Add(linkModel);
            }
            return Content(HttpStatusCode.OK, linkModels);
        }

        // POST: api/Link
        [Route("~/api/link/{companyId}")]
        public IHttpActionResult Post(int companyId, [FromBody]RouteDTO route)
        {
            try
            {
                RouteFacade.CreateRoute(route, companyId);
                return Content(HttpStatusCode.Created, route);
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.Message);
                return StatusCode(HttpStatusCode.NotFound);
            }
        }


    }
}
