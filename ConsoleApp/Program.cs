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
using System.Linq;
using BL.Services.Discounts;
using BL.DTOs.Discounts;
using BL.Services.Customers;
using BL.DTOs.Customers;
using BL.Services.Stations;
using BL.DTOs.Stations;
using BL.Services.Tickets;
using BL.DTOs.Tickets;
using BL.Services.Routes;
using BL.DTOs.Routes;
using BL.DTOs.RouteStations;
using DAL;

namespace ConsoleApp
{
    class Program
    {
        private static ICompanyService companyService;
        private static IVehicleService vehicleService;
        private static IDiscountService discountService;
        private static ICustomerService customerService;
        private static IStationService stationService;
        private static ITicketService ticketService;
        private static IRouteService routeService;

        private static readonly IWindsorContainer Container = new WindsorContainer();

        private static int companyId;
        static void Main(string[] args)
        {
            InitializeWindsorContainerAndMapper();

            companyService = Container.Resolve<ICompanyService>();
            try
            {
                companyService.CreateCompany(new CompanyDTO
                {
                    Name = "b",
                    CostPerKm = 2,
                    TimeToRedeem = new TimeSpan(0,5,0)
                });
            }
            catch (ArgumentException e)
            {
                //If debugging is stopped program will throw exception
                Console.WriteLine(e.Message);
            }
            finally
            {
                companyId = companyService.GetCompanyIdByName("b");
                companyService.DeleteCompany(companyId);
                companyService.CreateCompany(new CompanyDTO
                {
                    Name = "b",
                    CostPerKm = 2,
                    TimeToRedeem = new TimeSpan(0, 5, 0)
                });
            }

            companyId = companyService.GetCompanyIdByName("b");
            CompanyServiceTest();
            Console.WriteLine();
            VehicleServiceTest();
            Console.WriteLine();
            DiscountServiceTest();
            Console.WriteLine();
            CustomerServiceTest();
            Console.WriteLine();
            StationServiceTest();
            Console.WriteLine();
            TicketServiceTest();

            companyService.DeleteCompany(companyId);
        }

        private static void CompanyServiceTest()
        {   
            companyService = Container.Resolve<ICompanyService>();
            Console.WriteLine("Testing company service");
            var dto = new CompanyDTO
            {
                Name = "a",
                CostPerKm = 0
            };
            companyService.CreateCompany(dto);
            int companyId = companyService.GetCompanyIdByName("a");
            Console.WriteLine(companyId == companyService.GetCompanyById(companyId).ID ? "OK: Correct company returned" : "NOK: Companies do not match");
            var s = companyService.ListAllCompanies();
            Console.WriteLine(s.ToList().Count == 2 ? "OK: List works" : "NOK: list does not work");
            dto.Name = "c";
            dto.ID = companyId;
            var c = companyService.GetCompanyById(companyId);
            companyService.EditCompany(dto);
            Console.WriteLine(companyService.GetCompanyById(companyId).Name == "c" ? "OK: Changed" : "NOK: not changed");
            dto.Name = "b";
            try
            {
                companyService.EditCompany(dto);
                Console.WriteLine("NOK: Exception not thrown");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("OK: Exception thrown: " + e.Message);
            }
            companyService.DeleteCompany(companyId);
            s = companyService.ListAllCompanies();
            Console.WriteLine(s.ToList().Count == 1 ? "OK: Deleted" : "NOK: not deleted");
        }

        public static void VehicleServiceTest()
        {
            vehicleService = Container.Resolve<IVehicleService>();
            Console.WriteLine("Testing Vehicle service");
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
            vehicleService.CreateVehicle(v, companyId);
            vehicleService.CreateVehicle(v1, companyId);
            v.LicencePlate = "c";
            vehicleService.CreateVehicle(v, companyId);

            try
            {
                vehicleService.CreateVehicle(v1, companyId);
                Console.WriteLine("NOK: Should throw exception");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("OK: Correctly thrown exception: " + e.Message);
            }
            

            var list = vehicleService.ListVehicles(new BL.DTOs.Filters.VehicleFilter
            {
                CompanyId = companyId,
                VehicleType = VehicleType.Bus
            }, 2);
            var vehicleId1 = vehicleService.GetVehicleIdByLicencePlate("a");
            var vehicleId2 = vehicleService.GetVehicleIdByLicencePlate("b");
            var vehicleId3 = vehicleService.GetVehicleIdByLicencePlate("c");
            
            Console.WriteLine(list.TotalResultCount == 2 && list.ResultsPage.FirstOrDefault().LicencePlate.Equals("c") && list.ResultsPage.Count() == 1 ? "OK: Paged listing correct" : "NOK: Paged listing does not work");
            vehicleService.DeleteVehicle(vehicleId1);
            vehicleService.DeleteVehicle(vehicleId2);
            vehicleService.DeleteVehicle(vehicleId3);
            Console.WriteLine(vehicleService.ListVehicles(null).TotalResultCount == 0 ? "OK: Data deleted" : "NOK: data should be deleted");
        }

        private static void DiscountServiceTest()
        {
            discountService = Container.Resolve<IDiscountService>();
            Console.WriteLine("Testing discount service");
            discountService.CreateDiscount(new DiscountDTO
            {
                Value = 10,
                DiscountType = DiscountType.Student
            }, companyId);
            Console.WriteLine(discountService.ListDiscountsOfCompany(null, companyId).Count() == 1 ? "OK: List works" : "NOK: list does not work");
            Console.WriteLine(discountService.ListDiscountsOfCompany(DiscountType.Special, companyId).Count() == 0 ? "OK: List with filter works" : "NOK: list with filter does not work");
            var discountDto = discountService.GetDiscountById(discountService.ListDiscountsOfCompany(null, companyId).First().ID);
            discountDto.Value = 20;
            discountService.EditDiscount(discountDto);
            var a = discountService.ListDiscountsOfCompany(null, companyId).First();
            Console.WriteLine(a.Value == 20 ? "OK: edit works" : "NOK: edit does not work");
            discountDto.DiscountType = DiscountType.Special;
            try
            {
                discountService.EditDiscount(discountDto);
                Console.WriteLine("NOK: Exception not thrown");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("OK: Exception thrown: " + e.Message);
            }
            discountDto.DiscountType = DiscountType.Student;
            discountDto.Value = 10;
            var discountId = discountDto.ID;
            discountDto.ID = 0;
            try
            {
                discountService.CreateDiscount(discountDto, companyId);
                Console.WriteLine("NOK: Exception not thrown");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("OK: Exception thrown: " + e.Message);
            }
            discountService.DeleteDiscount(discountId);
            Console.WriteLine(discountService.ListDiscountsOfCompany(null, companyId).Count() == 0 ? "OK: Delete works" : "NOK: Delete does not work");
        }

        private static void CustomerServiceTest()
        {
            customerService = Container.Resolve<ICustomerService>();
            Console.WriteLine("Testing discount service");
            customerService.CreateCustomer(new CustomerDTO
            {
                FirstName = "Josef",
                LastName = "Kosta",
                Email = "p.kosta13@seznam.cz"
            });
            var customerDto = customerService.GetCustomerByEmail("p.kosta13@seznam.cz");

            Console.WriteLine(customerDto.FirstName.Equals("Josef") ? "OK: Get by email works" : "NOK: Get by email does not work");
            Console.WriteLine(customerService.ListAllCustomers(1).TotalResultCount == 1 ? "OK: Listing all customers works" : "NOK: Listing of all customers does not work");

            customerService.CreateCustomer(new CustomerDTO
            {
                FirstName = "Jose",
                LastName = "Kostb",
                Email = "p.kosta135@seznam.cz"
            });

            var list = customerService.ListAllCustomers(2);
            Console.WriteLine(list.TotalResultCount == 2 && list.ResultsPage.First().Email.Equals("p.kosta135@seznam.cz") ? "OK: Listing all customers with paging works" : "NOK: Listing of all customers with paging does not work");
            Console.WriteLine(customerService.ListCustomersByFilter(new BL.DTOs.Filters.CustomerFilter { FirstName = "Josef"}, 1).TotalResultCount == 1 ? "OK: Listing customers with filter works" : "NOK: Listing customers with filter does not work");
            Console.WriteLine(customerService.GetCustomerById(customerDto.ID).Email.Equals(customerDto.Email) ? "OK: Get by id works" : "NOK: Get by id does not work");

            customerDto.FirstName = "Pepa";
            customerService.EditCustomer(customerDto);
            Console.WriteLine(customerService.GetCustomerById(customerDto.ID).FirstName.Equals("Pepa") ? "OK: Edit customer works" : "NOK: Edit customer does not work");

            customerService.DeleteCustomer(customerDto.ID);
            customerService.DeleteCustomer(customerService.GetCustomerByEmail("p.kosta135@seznam.cz").ID);
            Console.WriteLine(customerService.ListAllCustomers(1).TotalResultCount == 0 ? "OK: Delete customers works" : "NOK: Delete customers does not work");
        }

        private static void TicketServiceTest()
        {
            customerService = Container.Resolve<ICustomerService>();
            ticketService = Container.Resolve<ITicketService>();
            stationService = Container.Resolve<IStationService>();
            routeService = Container.Resolve<IRouteService>();
            vehicleService = Container.Resolve<IVehicleService>();

            Console.WriteLine("Testing ticket and route service");
            routeService.CreateRoute(new RouteDTO
            {
                Name = "Tylova-Ceska"
            });  
            
            stationService.CreateStation(new StationDTO
            {
                Name = "Ceska",
                Town = "Brno"
            });
            stationService.CreateStation(new StationDTO
            {
                Name = "Tylova",
                Town = "Brno"
            });
            
            var stations = stationService.GetAllStationsByTown("Brno");
            var routeId = routeService.ListAllRoutes().FirstOrDefault().ID;

            foreach(var station in stations)
            {
                if (station.Name.Equals("Ceska"))
                {
                    routeService.AddRouteStation(station.ID, routeId, new RouteStationDTO
                    {
                        DepartFromFirstStation = null,
                        DistanceFromPreviousStation = 0,
                        TimeFromFirstStation = new TimeSpan(0,0,0),
                        TimeToNextStation = new TimeSpan(0, 5, 0),
            
                    });
                }
                else
                {
                    
                    routeService.AddRouteStation(station.ID, routeId, new RouteStationDTO
                    {
                        DepartFromFirstStation = null,
                        DistanceFromPreviousStation = 2,
                        TimeFromFirstStation = new TimeSpan(0, 5, 0),
                        TimeToNextStation = new TimeSpan(0, 0, 0),
            
                    });
                    
                }
            }
            
            vehicleService.CreateVehicle(new VehicleDTO
            {
                LicencePlate = "a",
                VehicleType = VehicleType.Bus,
                SeatCount = 2
            }, companyId);

            routeService.CreateSpecificRoute(routeId, DateTime.Now, vehicleService.GetVehicleIdByLicencePlate("a"));

            var listRouteStations = routeService.GetRouteStationsByRoute(routeId);
            int[] listRouteStationsForTicket = new int[2];
            int i = 0;
            foreach (var routeStation in listRouteStations)
            {
                if(routeStation.DepartFromFirstStation != null)
                {
                    listRouteStationsForTicket[i] = routeStation.ID;
                    i++;
                }
            }
            customerService.CreateCustomer(new CustomerDTO
            {
                FirstName = "Josef",
                LastName = "Kosta",
                Email = "p.kosta13@seznam.cz"
            });
            var customerDto = customerService.GetCustomerByEmail("p.kosta13@seznam.cz");
            ticketService.CreateTicket(customerDto.ID, companyId, new TicketDTO { Departure = DateTime.Now + (new TimeSpan(2,0,0)),  RouteName = "Ceska" }, routeService.ListEmptyProgramsOfRouteStations(listRouteStationsForTicket).First());

            var ticketId = ticketService.ListAllTickets().FirstOrDefault().ID;
            ticketService.TicketReservation(ticketId);

            Console.WriteLine(ticketService.GetTicketById(ticketId).Price == 4 ? "OK: Ticket reservation works... sorry deadline in 1 hour and I managed to create ticket 10 minutes ago so no time for clever tests" : "NOK:Ticket does not work");

            discountService = Container.Resolve<IDiscountService>();
            discountService.CreateDiscount(new DiscountDTO
            {
                Value = 50,
                DiscountType = DiscountType.Student
            }, companyId);

            var discountId = discountService.ListDiscountsOfCompany(null, companyId).First().ID;
            ticketService.ClaimDiscount(ticketId, discountId, false, null);
            Console.WriteLine(ticketService.GetTicketById(ticketId).Price == 2 ? "OK: Ticket claim discount works... 10 min to deadline...." : "NOK:Ticket does not work");
            ticketService.TicketRefund(ticketId);

            Console.WriteLine(ticketService.GetTicketById(ticketId).IsRefunded ? "OK: Ticket refund works... idk if this is worth 2 credits :/" : "NOK:Ticket does not work");
            routeService.DeleteRoute(routeId);
            foreach (var station in stations)
            {
                stationService.DeleteStation(station.ID);
            }
            customerService.DeleteCustomer(customerDto.ID);
        }

        private static void StationServiceTest()
        {
            Console.WriteLine("Testing station service");
            stationService = Container.Resolve<IStationService>();
            stationService.CreateStation(new StationDTO
            {
                Name = "Ceska",
                Town = "Brno"
            });

            var brnoStations = stationService.GetAllStationsByTown("Brno");
            if (brnoStations == null || !brnoStations.Any())
            {
                Console.WriteLine("NOK: Not found any station in Brno");
            }
            else if (brnoStations.Where(station => station.Name == "Ceska") == null || !brnoStations.Any())
            {
                Console.WriteLine("NOK: Station Ceska in Brno not created");
            }
            else
            {
                Console.WriteLine("OK");
            }
            var stationCeska = stationService.GetStationById(brnoStations.First().ID);
            if (stationCeska.Name != "Ceska" || stationCeska.Town != "Brno")
            {
                Console.WriteLine("NOK: Not found station Ceska in Brno by Id");
            }
            else
            {
                Console.WriteLine("OK");
            }
            stationService.DeleteStation(stationCeska.ID);
            if (stationService.GetStationById(stationCeska.ID) != null)
            {
                Console.WriteLine("NOK: Station not deleted");
            }
            else
            {
                Console.WriteLine("OK");
            }

        }


        //data are not deleted, depends on specific id
        private static void FindRouteTest()
        {
            stationService = Container.Resolve<IStationService>();
            routeService = Container.Resolve<IRouteService>();
            vehicleService = Container.Resolve<IVehicleService>();
            companyService = Container.Resolve<ICompanyService>();
            stationService.CreateStation(new StationDTO
            {
                Name = "Ceska",
                Town = "Brno"
            });
            stationService.CreateStation(new StationDTO
            {
                Name = "Hlavni",
                Town = "Brno"
            });
            stationService.CreateStation(new StationDTO
            {
                Name = "Olomoucka",
                Town = "Brno"
            });
            stationService.CreateStation(new StationDTO
            {
                Name = "Klusackova",
                Town = "Brno"
            });

            routeService.CreateRoute(new RouteDTO
            {
                Name = "First Route"
            });

            routeService.CreateRoute(new RouteDTO
            {
                Name = "Second Route"
            });

            routeService.AddRouteStation(4, 2, new RouteStationDTO
            {
                DepartFromFirstStation = DateTime.Today,
                DistanceFromPreviousStation = 2,
                TimeFromFirstStation = new TimeSpan(0, 5, 0),
                TimeToNextStation = new TimeSpan(0, 0, 0),
                Order = 1
            });

            routeService.AddRouteStation(5, 2, new RouteStationDTO
            {
                DepartFromFirstStation = DateTime.Today,
                DistanceFromPreviousStation = 2,
                TimeFromFirstStation = new TimeSpan(0, 5, 0),
                TimeToNextStation = new TimeSpan(0, 0, 0),
                Order = 2
            });
            routeService.AddRouteStation(6, 2, new RouteStationDTO
            {
                DepartFromFirstStation = DateTime.Today,
                DistanceFromPreviousStation = 2,
                TimeFromFirstStation = new TimeSpan(0, 5, 0),
                TimeToNextStation = new TimeSpan(0, 0, 0),
                Order = 3
            });
            routeService.FindRoutesWithStations(1, 3, DateTime.Today);
        }

        private static void InitializeWindsorContainerAndMapper()
        {
            Container.Install(new BLInstaller());

            Mapping.ConfigureMapping();
        }
    }
}
