using AutoMapper.QueryableExtensions;
using BL.AppInfrastructure;
using BL.DTOs.Routes;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Queries
{
    public class RouteListAllQuery : AppQuery<RouteDTO>
    {
        public RouteListAllQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<RouteDTO> GetQueryable()
        {
            return Context.Routes.ProjectTo<RouteDTO>();
        }
    }
}
