using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using BL.DTOs.Companies;
using BL.Facades;
using System.Diagnostics;

namespace API.Controllers
{
    public class CompanyController : ApiController
    {
        public CompanyFacade CompanyFacade { get; set; }

        /// <summary>
        /// GET request for company, list all existed companies
        /// </summary>
        /// <returns>All companies</returns>
        //GET: api/Company
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.OK, CompanyFacade.ListAllCompanies());
        }


        /// <summary>
        /// List specific company
        /// </summary>
        /// <param name="id">Id of company</param>
        /// <returns>Company with given id</returns>
        // GET: api/Company/5
        public IHttpActionResult Get(int id)
        {
            CompanyDTO company;
            if(id <= 0)
            {
                return NotFound();
            }
            company = CompanyFacade.GetCompanyById(id);
            return Content(HttpStatusCode.OK, company);
        }

        /// <summary>
        /// Creates new company
        /// </summary>
        /// <param name="company">company to be created</param>
        /// <returns>OK, if created</returns>
        // POST: api/Company
        public IHttpActionResult Post([FromBody]CompanyDTO company)
        {
            try
            {
                //CompanyDTO company = JsonConvert.DeserializeObject<CompanyDTO>(value);
                CompanyFacade.CreateCompany(company);
                return Content(HttpStatusCode.Created, company);
            }
            catch (JsonException)
            {
                //Debug.WriteLine($"Company API post failed to deserialized {value}");
                return StatusCode(HttpStatusCode.BadRequest);
            }
            catch (NullReferenceException e)
            {
                Debug.WriteLine(e.Message);
                return StatusCode(HttpStatusCode.NotFound);
            }
        }


        /// <summary>
        /// Update of specific company
        /// </summary>
        /// <param name="id">id of company</param>
        /// <param name="company">Updated attributes</param>
        /// <returns>OK, if found and updated</returns>
        // PUT: api/Company/5
        public IHttpActionResult Put(int id, [FromBody]CompanyDTO company)
        {
            try
            {
                if (id <= 0)
                {
                    return NotFound();
                }
                company.ID = id;
                CompanyFacade.EditCompany(company);
                return Content(HttpStatusCode.OK, company);
            }
            catch(NullReferenceException e)
            {
                Debug.WriteLine(e.Message);
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
    }
}
