using AutoMapper;
using BL.DTOs.Companies;
using BL.DTOs.Vehicles;
using BL.DTOs.Seats;
using BL.DTOs.Discounts;
using BL.DTOs.Customers;
using BL.DTOs.Tickets;
using BL.DTOs.Programs;
using DAL.Entities;
using Riganti.Utils.Infrastructure.EntityFramework;
using BL.DTOs;
using BL.DTOs.Stations;
using BL.DTOs.Routes;
using BL.DTOs.RouteStations;

namespace BL
{
    public static class Mapping
    {
        public static void ConfigureMapping()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Company, CompanyDTO>().ReverseMap();
                config.CreateMap<Vehicle, VehicleDTO>().ReverseMap();
                config.CreateMap<Seat, SeatDTO>().ReverseMap();
                config.CreateMap<Customer, CustomerDTO>().ReverseMap();
                config.CreateMap<Discount, DiscountDTO>().ReverseMap();
                config.CreateMap<Ticket, TicketDTO>().ReverseMap();
                config.CreateMap<Program, ProgramDTO>().ReverseMap();
                config.CreateMap<Station, StationDTO>().ReverseMap();
                config.CreateMap<Route, RouteDTO>().ReverseMap();
                config.CreateMap<RouteStation, RouteStationDTO>().ReverseMap();
            });
        }
    }
}
