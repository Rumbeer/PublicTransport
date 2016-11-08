using BL;
using BL.Services.Companies;
using BL.Services.Vehicles;
using Castle.Windsor;
using BL.DTOs.Companies;
using BL.DTOs.Vehicles;
using BL.DTOs.Seats;
using DAL.Enum;
using System.Collections.Generic;
using System;

namespace ConsoleApp
{
    class Program
    {
        private static ICompanyService companyService;
        private static IVehicleService vehicleService;

        private static readonly IWindsorContainer Container = new WindsorContainer();

        static void Main(string[] args)
        {
            // configure DI
            InitializeWindsorContainerAndMapper();
            CompanyServiceTest();
            VehicleServiceTest();
        }

        private static void CompanyServiceTest()
        {
            companyService = Container.Resolve<ICompanyService>();
            companyService.CreateCompany(new CompanyDTO
            {
                Name = "a",
                CostPerKm = 0
            });
            int companyId = companyService.GetCompanyIdByName("a");
            if(companyId == companyService.GetCompanyById(companyId).ID)
            {
                System.Console.WriteLine("Correct company returned");
            }
            companyService.DeleteCompany(companyId);
            var s = companyService.ListAllCompanies();
        }

        public static void VehicleServiceTest()
        {
            vehicleService = Container.Resolve<IVehicleService>();
            //            vehicleService.CreateVehicle(new VehicleDTO
            //            {
            //                LicencePlate = "a",
            //                VehicleType = VehicleType.Bus,
            //                SeatCount = 2
            //            }, 2);
            var v = new VehicleDTO
            {
                LicencePlate = "a",
                VehicleType = VehicleType.Bus,
                SeatCount = 2
            };
            var v1 = new VehicleDTO
            {
                LicencePlate = "b",
                VehicleType = VehicleType.Train,
                SeatCount = 2
            };
            vehicleService.CreateVehicle(v, 2);
            vehicleService.CreateVehicle(v1, 2);
            v.LicencePlate = "c";
            vehicleService.CreateVehicle(v, 2);

            try
            {
                vehicleService.CreateVehicle(v1, 2);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Correctly thrown exception: " + e.Message);
            }
            

            var list = vehicleService.ListVehicles(new BL.DTOs.Filters.VehicleFilter
            {
                CompanyId = 1,
                VehicleType = VehicleType.Bus
            });
            var vehicleId1 = vehicleService.GetVehicleIdByLicencePlate("a");
            var vehicleId2 = vehicleService.GetVehicleIdByLicencePlate("b");
            var vehicleId3 = vehicleService.GetVehicleIdByLicencePlate("c");

            list = vehicleService.ListVehicles(new BL.DTOs.Filters.VehicleFilter
            {
                CompanyId = 2,
                VehicleType = VehicleType.Bus
            }, 2);
            vehicleService.DeleteVehicle(vehicleId1);
            vehicleService.DeleteVehicle(vehicleId2);
            vehicleService.DeleteVehicle(vehicleId3);
            if (vehicleService.ListVehicles(null).TotalResultCount == 0)
            {
                Console.WriteLine("Data deleted");
            }
        }

        private static void InitializeWindsorContainerAndMapper()
        {
            Container.Install(new BLInstaller());

            Mapping.ConfigureMapping();
        }
    }
}
