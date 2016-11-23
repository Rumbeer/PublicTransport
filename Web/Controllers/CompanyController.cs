using BL.DTOs.Companies;
using BL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class CompanyController : Controller
    {
        public CompanyFacade CompanyFacade { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListAllCompanies()
        {
            var list = CompanyFacade.ListAllCompanies();
            return View(list);
        }

        public ActionResult Create()
        {
            return View(new CompanyDTO());
        }

        [HttpPost]
        public ActionResult CreateCompany(CompanyDTO company)
        {
            try
            {
                CompanyFacade.CreateCompany(company);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View(company);
            }
            return RedirectToAction("ListAllCompanies");
        }

        public ActionResult DeleteCompany(int id)
        {
            CompanyFacade.DeleteCompany(id);
            return RedirectToAction("ListAllCompanies");
        }

        public ActionResult EditCompany(int id)
        {
            return View(CompanyFacade.GetCompanyById(id));
        }

        [HttpPost]
        public ActionResult EditCompany(CompanyDTO company)
        {
            try
            {
                CompanyFacade.EditCompany(company);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View(company);
            }
            return RedirectToAction("ListAllCompanies");
        }
    }
}