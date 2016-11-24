using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.DTOs.Companies;
using BL.DTOs.Filters;
using BL.Queries;
using BL.Repositories;
using DAL.Entities;
using System;

namespace BL.Services.Companies
{
    public class CompanyService : AppService, ICompanyService
    {

        #region Dependencies

        private readonly CompanyRepository companyRepository;

        private readonly CompanyListQuery companyListQuery;

        public CompanyService(CompanyRepository companyRepository, CompanyListQuery companyListQuery)
        {
            this.companyRepository = companyRepository;
            this.companyListQuery = companyListQuery;
        }
        #endregion

        public void CreateCompany(CompanyDTO companyDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                companyListQuery.Filter = new CompanyFilter { Name = companyDto.Name };
                if (companyListQuery.Execute().SingleOrDefault() != null)
                {
                    throw new ArgumentException("Company service - CreateCompany(...) company with this name already exists");
                }
                var company = Mapper.Map<Company>(companyDto);

                companyRepository.Insert(company);
                uow.Commit();
            }
        }

        public void EditCompany(CompanyDTO companyDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var company = companyRepository.GetById(companyDto.ID, c => c.Vehicles, c => c.Routes, c => c.Discounts);
                if (!company.Name.Equals(companyDto.Name) && GetCompanyIdByName(companyDto.Name) != 0)
                {
                    throw new ArgumentException("Company service - EditCompany(...) company with that name already exists");
                }
                Mapper.Map(companyDto, company);

                companyRepository.Update(company);
                uow.Commit();
            }
        }

        public void DeleteCompany(int companyId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                companyRepository.Delete(companyId);
                uow.Commit();
            }
        }

        public CompanyDTO GetCompanyById(int companyId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var company = companyRepository.GetById(companyId);
                return company != null ? Mapper.Map<CompanyDTO>(company) : null;
            }
        }

        public int GetCompanyIdByName(string companyName)
        {
            using (UnitOfWorkProvider.Create())
            {
                companyListQuery.Filter = new CompanyFilter { Name = companyName };
                var company = companyListQuery.Execute().SingleOrDefault();
                return company != null ? company.ID : 0;
            }
        }

        public IEnumerable<CompanyDTO> ListAllCompanies()
        {
            using (UnitOfWorkProvider.Create())
            {
                companyListQuery.Filter = null;
                return companyListQuery.Execute() ?? new List<CompanyDTO>();
            }
        }
    }
}
