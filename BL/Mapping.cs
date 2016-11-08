using AutoMapper;
using BL.DTOs.Companies;
using BL.DTOs.Vehicles;
using BL.DTOs.Seats;
using BL.DTOs.Discounts;
using BL.DTOs.Customers;
using DAL.Entities;
using Riganti.Utils.Infrastructure.EntityFramework;

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
                config.CreateMap<Customer, CustomerDTO>()
                    .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Account.Email))
                    .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.Account.FirstName))
                    .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.Account.LastName))
                    .ForMember(dest => dest.PhoneNumber, opts => opts.MapFrom(src => src.Account.MobilePhoneNumber))
                    .ForMember(dest => dest.Address, opts => opts.MapFrom(src => src.Account.Address))
                    .ForMember(dest => dest.BirthDate, opts => opts.MapFrom(src => src.Account.BirthDate))
                    .ReverseMap();
                config.CreateMap<Discount, DiscountDTO>().ReverseMap();
            });
        }
    }
}
