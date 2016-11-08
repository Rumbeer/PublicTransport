using System.Collections.Generic;
using BL.DTOs.Companies;

namespace BL.Services.Companies
{
    public interface ICompanyService
    {
        /// <summary>
        /// Creates new company
        /// </summary>
        /// <param name="companyDto">company details</param>
        void CreateCompany(CompanyDTO companyDto);
        
        /// <summary>
        /// Updates company
        /// </summary>
        /// <param name="companyDto">company details</param>
        void EditCompany(CompanyDTO companyDto);

        /// <summary>
        /// Deletes specific company
        /// </summary>
        /// <param name="companyId">id of company to be deleted</param>
        void DeleteCompany(int companyId);

        /// <summary>
        /// Gets companyDTO of specific id
        /// </summary>
        /// <param name="companyId">id of company to be returned</param>
        /// <returns>companyDTO which has companyId</returns>
        CompanyDTO GetCompanyById(int companyId);

        /// <summary>
        /// Gets company id from given name
        /// </summary>
        /// <param name="name">company name</param>
        /// <returns>id of company with specified name</returns>
        int GetCompanyIdByName(string companyName);

        /// <summary>
        /// Gets all companies
        /// </summary>
        /// <returns>all available companies</returns>
        IEnumerable<CompanyDTO> ListAllCompanies();
    }
}
