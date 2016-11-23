using BL.DTOs.Companies;
using BL.Facades;
using System;
using BL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Helper.Enums;
using BL.DTOs.Discounts;
using BL.DTOs.Vehicles;

namespace Web.Controllers
{

    public class CompanyController : Controller
    {
        public CompanyFacade CompanyFacade { get; set; }

        public ActionResult Index()
        {
            return View();
        }
        #region Company Methods

        public ActionResult ListAllCompanies()
        {
            var list = CompanyFacade.ListAllCompanies();
            return View(list);
        }

        public ActionResult CreateCompany()
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

        public ActionResult CompanyDetails(int id)
        {
            return View(GetCompanyDetails(id));
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

        #endregion

        #region Discount Methods

        public ActionResult CreateDiscount(int companyId)
        {
            return View(new CreateDiscountModel()
            {
                CompanyId = companyId,
                Discount = new DiscountDTO()
            });
        }

        [HttpPost]
        public ActionResult CreateDiscount(CreateDiscountModel model)
        {
            try
            {
                CompanyFacade.CreateDiscount(model.Discount, model.CompanyId);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View(model);
            }
            if(model.Discount.Code != null && !model.Discount.DiscountType.Equals(DiscountType.Special))
            {
                ViewBag.Message = "This type of discount should not have code!";
                return View(model);
            }
            return RedirectToAction("CompanyDetails", new { id = model.CompanyId});
        }

        public ActionResult DeleteDiscount(int id, int companyId)
        {
            CompanyFacade.DeleteDiscount(id);
            return RedirectToAction("CompanyDetails", new { id = companyId });
        }

        public ActionResult EditDiscount(int id, int companyId)
        {
            return View(new CreateDiscountModel()
            {
                CompanyId = companyId,
                Discount = CompanyFacade.GetDiscountById(id)
            });
        }

        [HttpPost]
        public ActionResult EditDiscount(CreateDiscountModel model)
        {
            try
            {
                CompanyFacade.EditDiscount(model.Discount);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View(model);
            }
            if (model.Discount.Code != null && !model.Discount.DiscountType.Equals(DiscountType.Special))
            {
                ViewBag.Message = "This type of discount should not have code!";
                return View(model);
            }
            return RedirectToAction("CompanyDetails", new { id = model.CompanyId });
        }

        #endregion

        #region Vehicle Methods

        public ActionResult CreateVehicle()
        {
            return View(new VehicleDTO());
        }

        #endregion

        #region Helper
        private CompanyDetailsModel GetCompanyDetails(int companyId)
        {
            var model = new CompanyDetailsModel();
            model.Company = CompanyFacade.GetCompanyById(companyId);
            model.Discounts = CompanyFacade.ListDiscountsOfCompany(null, companyId);
            return model;
        }
        #endregion
    }
}