using BL.DTOs.Companies;
using BL.DTOs.Discounts;
using BL.DTOs.Filters;
using BL.DTOs.Seats;
using BL.DTOs.Vehicles;
using BL.Services.Companies;
using BL.Services.Discounts;
using BL.Services.Vehicles;
using BL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Facades
{
    public class CompanyFacade
    {
        private readonly ICompanyService companyService;
        private readonly IDiscountService discountService;
        private readonly IVehicleService vehicleService;

        public CompanyFacade(ICompanyService companyService, IDiscountService discountService, IVehicleService vehicleService)
        {
            this.companyService = companyService;
            this.discountService = discountService;
            this.vehicleService = vehicleService;
        }

        public int VehiclePageSize => vehicleService.PageSize;

        /// <summary>
        /// Creates new company
        /// </summary>
        /// <param name="companyDto">company details</param>
        public void CreateCompany(CompanyDTO companyDto)
        {
            companyService.CreateCompany(companyDto);
        }

        /// <summary>
        /// Updates company
        /// </summary>
        /// <param name="companyDto">company details</param>
        public void EditCompany(CompanyDTO companyDto)
        {
            companyService.EditCompany(companyDto);
        }

        /// <summary>
        /// Deletes specific company
        /// </summary>
        /// <param name="companyId">id of company to be deleted</param>
        public void DeleteCompany(int companyId)
        {
            companyService.DeleteCompany(companyId);
        }

        /// <summary>
        /// Gets companyDTO of specific id
        /// </summary>
        /// <param name="companyId">id of company to be returned</param>
        /// <returns>companyDTO which has companyId</returns>
        public CompanyDTO GetCompanyById(int companyId)
        {
            return companyService.GetCompanyById(companyId);
        }

        /// <summary>
        /// Gets company id from given name
        /// </summary>
        /// <param name="name">company name</param>
        /// <returns>id of company with specified name</returns>
        public int GetCompanyIdByName(string companyName)
        {
            return companyService.GetCompanyIdByName(companyName);
        }

        /// <summary>
        /// Gets all companies
        /// </summary>
        /// <returns>all available companies</returns>
        public IEnumerable<CompanyDTO> ListAllCompanies()
        {
            return companyService.ListAllCompanies();
        }

        /// <summary>
        /// Created new Discount
        /// </summary>
        /// <param name="discountDto">discount details</param>
        /// <param name="companyId">id of company</param>
        public void CreateDiscount(DiscountDTO discountDto, int companyId)
        {
            discountService.CreateDiscount(discountDto, companyId);
        }

        /// <summary>
        /// Updates discount
        /// </summary>
        /// <param name="discountDto">discount details</param>
        public void EditDiscount(DiscountDTO discountDto)
        {
            discountService.EditDiscount(discountDto);
        }

        /// <summary>
        /// Deletes discount
        /// </summary>
        /// <param name="discountId">id of dicount</param>
        public void DeleteDiscount(int discountId)
        {
            discountService.DeleteDiscount(discountId);
        }

        /// <summary>
        /// Gets discount of specific id
        /// </summary>
        /// <param name="discountId">id of discount</param>
        /// <returns></returns>
        public DiscountDTO GetDiscountById(int discountId)
        {
            return discountService.GetDiscountById(discountId);
        }

        /// <summary>
        /// Gets discounts of specific company
        /// </summary>
        /// <param name="companyId">id of company</param>
        /// <param name="discountType">type of discount</param>
        /// <returns></returns>
        public IEnumerable<DiscountDTO> ListDiscountsOfCompany(DiscountType? discountType, int companyId)
        {
            return discountService.ListDiscountsOfCompany(discountType, companyId);
        }

        /// <summary>
        /// Creates vehicle with its seats
        /// </summary>
        /// <param name="vehicleDto">vehicle details</param>
        /// <param name="companyId">id of company</param>
        public void CreateVehicle(VehicleDTO vehicleDto, int companyId)
        {
            vehicleService.CreateVehicle(vehicleDto, companyId);
        }

        /// <summary>
        /// Gets vehicles by filter
        /// </summary>
        /// <param name="filter">vehicle filter</param>
        /// <param name="page">requested page</param>
        /// <returns></returns>
        public VehicleListQueryResultDTO ListVehicles(VehicleFilter filter, int page = 1)
        {
            return vehicleService.ListVehicles(filter, page);
        }

        /// <summary>
        /// Gets vehicle by id
        /// </summary>
        /// <param name="vehicleId">id of a vehicle</param>
        /// <returns></returns>
        public VehicleDTO GetVehicleById(int vehicleId)
        {
            return vehicleService.GetVehicleById(vehicleId);
        }

        /// <summary>
        /// Gets id of vehicle by licence plate
        /// </summary>
        /// <param name="licencePlate">licence plate of a vehicle</param>
        /// <returns></returns>
        public int GetVehicleIdByLicencePlate(string licencePlate)
        {
            return vehicleService.GetVehicleIdByLicencePlate(licencePlate);
        }

        /// <summary>
        /// Deletes vehicle
        /// </summary>
        /// <param name="vehicleId"> id of vehicle to be deleted</param>
        public void DeleteVehicle(int vehicleId)
        {
            vehicleService.DeleteVehicle(vehicleId);
        }

        /// <summary>
        /// Gets all seats of specified vehicle
        /// </summary>
        /// <param name="vehicleId">id of vehicle</param>
        /// <returns></returns>
        public IEnumerable<SeatDTO> GetVehicleSeats(int vehicleId)
        {
            return GetVehicleSeats(vehicleId);
        }
    }
}
