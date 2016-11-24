using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Sample API calls (via Postman) are available at API(project)/Test/DemoEshopRatingAPI.postman_collection";
        }
    }
}
