using System.Collections.Generic;
using BL.DTOs.Vehicles;
using BL.DTOs.Seats;
using BL.DTOs.Filters;

namespace BL.Services.Vehicles
{
    public interface IVehicleService
    {
        int PageSize { get; }

        /// <summary>
        /// Creates vehicle with its seats
        /// </summary>
        /// <param name="vehicleDto">vehicle details</param>
        /// <param name="companyId">id of company</param>
        void CreateVehicle(VehicleDTO vehicleDto, int companyId);

        /// <summary>
        /// Gets vehicles by filter
        /// </summary>
        /// <param name="filter">vehicle filter</param>
        /// <param name="page">requested page</param>
        /// <returns></returns>
        VehicleListQueryResultDTO ListVehicles(VehicleFilter filter, int page = 1);

        /// <summary>
        /// Gets vehicle by id
        /// </summary>
        /// <param name="vehicleId">id of a vehicle</param>
        /// <returns></returns>
        VehicleDTO GetVehicleById(int vehicleId, int? companyId);

        /// <summary>
        /// Gets id of vehicle by licence plate
        /// </summary>
        /// <param name="licencePlate">licence plate of a vehicle</param>
        /// <returns></returns>
        int GetVehicleIdByLicencePlate(string licencePlate);

        /// <summary>
        /// Deletes vehicle
        /// </summary>
        /// <param name="vehicleId"> id of vehicle to be deleted</param>
        void DeleteVehicle(int vehicleId);

        /// <summary>
        /// Gets all seats of specified vehicle
        /// </summary>
        /// <param name="vehicleId">id of vehicle</param>
        /// <returns></returns>
        IEnumerable<SeatDTO> GetVehicleSeats(int vehicleId);

        /// <summary>
        /// Gets licence plates of all vehicles in given company
        /// </summary>
        /// <param name="companyId">id of company</param>
        /// <returns></returns>
        IEnumerable<string> GetVehicleLicencePlates(int companyId);


    }
}
