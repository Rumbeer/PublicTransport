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
using BL.DTOs.UserAccount;

namespace BL
{
    public static class Mapping
    {
        public static void ConfigureMapping()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Company, CompanyDTO>().ReverseMap();
                config.CreateMap<Vehicle, VehicleDTO>()
                    .ForMember(dest => dest.VehicleType, option => option.MapFrom(vehicle => (BL.Enum.VehicleType)vehicle.VehicleType))
                    .ReverseMap();

                config.CreateMap<Seat, SeatDTO>().ReverseMap();
                config.CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Account.Email))
                    .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.Account.FirstName))
                    .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.Account.LastName))
                    .ForMember(dest => dest.PhoneNumber, opts => opts.MapFrom(src => src.Account.MobilePhoneNumber))
                    .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Account.Address))
                    .ForMember(dest => dest.BirthDate, opts => opts.MapFrom(src => src.Account.BirthDate))
                    .ReverseMap();

                config.CreateMap<UserAccount, UserAccountDTO>().ReverseMap();

                config.CreateMap<UserRegistrationDTO, UserAccount>();

                config.CreateMap<Discount, DiscountDTO>()
                    .ForMember(dest => dest.DiscountType, option => option.MapFrom(discount => (BL.Enum.DiscountType)discount.DiscountType))
                    .ReverseMap();

                config.CreateMap<Ticket, TicketDTO>().ReverseMap();
                config.CreateMap<Program, ProgramDTO>().ReverseMap();
                config.CreateMap<Station, StationDTO>().ReverseMap();
                config.CreateMap<Route, RouteDTO>().ReverseMap();
                config.CreateMap<RouteStation, RouteStationDTO>().ReverseMap();
            });
        }
    }
}
