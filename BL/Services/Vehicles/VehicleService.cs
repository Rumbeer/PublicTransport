using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.DTOs.Vehicles;
using BL.DTOs.Filters;
using BL.Queries;
using BL.Repositories;
using DAL.Entities;
using System;
using BL.DTOs.Seats;
using Riganti.Utils.Infrastructure.Core;

namespace BL.Services.Vehicles
{
    public class VehicleService : AppService, IVehicleService
    {
        //for tests the page size is 1
        public int PageSize => 5;

        #region Dependencies

        private readonly CompanyRepository companyRepository;

        private readonly SeatRepository seatRepository;

        private readonly SeatListQuery seatListQuery;

        private readonly VehicleRepository vehicleRepository;

        private readonly VehicleListQuery vehicleListQuery;

        public VehicleService(SeatRepository seatRepository, VehicleListQuery vehicleListQuery, VehicleRepository vehicleRepository, CompanyRepository companyRepository, SeatListQuery seatListQuery)
        {
            this.seatListQuery = seatListQuery;
            this.companyRepository = companyRepository;
            this.vehicleRepository = vehicleRepository;
            this.seatRepository = seatRepository;
            this.vehicleListQuery = vehicleListQuery;
        }
        #endregion

        public void CreateVehicle(VehicleDTO vehicleDto, int companyId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var query = vehicleListQuery;
                query.ClearSortCriterias();
                query.Filter = new VehicleFilter { LicencePlate = vehicleDto.LicencePlate };
                query.AddSortCriteria("LicencePlate", SortDirection.Ascending);
                query.Skip = 0;
                if (query.Execute().SingleOrDefault() != null)
                {
                    throw new ArgumentException("Vehicle service - CreateVehicle(...) vehicle with this licence plate already exists");
                }
                var vehicle = Mapper.Map<Vehicle>(vehicleDto);

                var company = companyRepository.GetById(companyId);
                if (company == null)
                {
                    throw new NullReferenceException("Vehicle service - CreateVehicle(...) company cant be null");
                }
                vehicle.Company = company;
                vehicle.Seats = new List<Seat>();
                for (int i = 1; i <= vehicleDto.SeatCount; i++)
                {
                    var newSeat = CreateSeat(vehicle, i);
                    vehicle.Seats.Add(newSeat);
                    seatRepository.Insert(newSeat);
                }

                vehicleRepository.Insert(vehicle);
                uow.Commit();
            }
        }

        public VehicleListQueryResultDTO ListVehicles(VehicleFilter filter, int page = 1)
        {
            using (UnitOfWorkProvider.Create())
            {
                var query = vehicleListQuery;
                query.ClearSortCriterias();
                query.Filter = filter;

                query.Skip = Math.Max(0, page - 1) * PageSize;
                query.Take = PageSize;
                query.AddSortCriteria("LicencePlate", SortDirection.Ascending);

                return new VehicleListQueryResultDTO
                {
                    RequestedPage = page,
                    TotalResultCount = query.GetTotalRowCount(),
                    ResultsPage = query.Execute(),
                    Filter = filter
                };
            }
        }

        public VehicleDTO GetVehicleById(int vehicleId, int ?companyId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var vehicle = vehicleRepository.GetById(vehicleId, v => v.Company);
                if(companyId != null && vehicle != null)
                {
                    if(vehicle.Company.ID != companyId)
                    {
                        return null;
                    }
                }
                return vehicle != null ? Mapper.Map<VehicleDTO>(vehicle) : null;
            }
        }

        public int GetVehicleIdByLicencePlate(string licencePlate)
        {
            using (UnitOfWorkProvider.Create())
            {
                var query = vehicleListQuery;
                query.ClearSortCriterias();
                query.Filter = new VehicleFilter { LicencePlate = licencePlate };
                query.AddSortCriteria("LicencePlate", SortDirection.Ascending);
                query.Skip = 0;
                var vehicle = vehicleListQuery.Execute().SingleOrDefault();
                return vehicle != null ? vehicle.ID : 0;
            }
        }

        public void DeleteVehicle(int vehicleId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                vehicleRepository.Delete(vehicleId);
                uow.Commit();
            }
        }

        private Seat CreateSeat(Vehicle vehicle, int seatNumber)
        {
            var seat = new Seat()
            {
                SeatNumber = seatNumber,
                Vehicle = vehicle
            };
            return seat;
        }

        public IEnumerable<SeatDTO> GetVehicleSeats(int vehicleId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if(vehicleRepository.GetById(vehicleId) == null)
                {
                    throw new NullReferenceException("Vehicle service - GetVehicleSeats(...) vehicle cant be null");
                }

                seatListQuery.Filter = new SeatFilter { VehicleId = vehicleId };
                var list = seatListQuery.Execute();
                return list.Select(seat => Mapper.Map<SeatDTO>(seat));
            }
        }

        public IEnumerable<string> GetVehicleLicencePlates(int companyId)
        {
            using (UnitOfWorkProvider.Create())
            {
                if (companyRepository.GetById(companyId) == null)
                {
                    throw new NullReferenceException("Vehicle service - GetVehicleLicencePlates(...) company cant be null");
                }
                var query = vehicleListQuery;
                query.ClearSortCriterias();
                query.Filter = new VehicleFilter
                {
                    CompanyId = companyId
                };

                query.Skip = 0;
                query.AddSortCriteria("LicencePlate", SortDirection.Ascending);
                var list = vehicleListQuery.Execute();
                var result = new List<string>();
                foreach(var vehicle in list)
                {
                    result.Add(vehicle.LicencePlate);
                }
                return result;
            }
        }
    }
}
