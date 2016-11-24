using AutoMapper.QueryableExtensions;
using BL.AppInfrastructure;
using BL.DTOs.Routes;
using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Queries
{
    public class CompanyRouteListQuery : AppQuery<RouteDTO>
    {
        public int CompanyId { get; set; }

        public CompanyRouteListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<RouteDTO> GetQueryable()
        {
            IQueryable<Route> query = Context.Routes.Include(nameof(Route.Company));

            if (CompanyId > 0)
            {
                query = query.Where(route => route.Company.ID == CompanyId);
            }

            return query.ProjectTo<RouteDTO>();
        }
    }
}

