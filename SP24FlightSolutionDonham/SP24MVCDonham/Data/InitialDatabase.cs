using Microsoft.AspNetCore.Identity;
using SP24ClassLibraryDonham;
using System.Runtime.ConstrainedExecution;

namespace SP24MVCDonham.Data
{
    public class InitialDatabase
    {
        public static async Task InitializeDatabase(IServiceProvider services)
        {
            //Gets access to DB
            ApplicationDbContext database =
                services.GetRequiredService<ApplicationDbContext>();

            //Airport Data
            if (!database.Airports.Any())
            {
                Airport airport = new Airport("Pittsburgh", "PIT", 100);
                database.Airports.Add(airport);
                airport = new Airport("Orlando", "ORL", 200);
                database.Airports.Add(airport);
                airport = new Airport("John F Kennedy", "JFK", 300);
                database.Airports.Add(airport);
                database.SaveChanges();
            }

            //Airline
            if (!database.Airlines.Any())
            {
                Airline airline = new Airline("Southwest", "LUV");
                database.Airlines.Add(airline);
                airline = new Airline("Delta");
                database.Airlines.Add(airline);
                airline = new Airline("United");
                database.Airlines.Add(airline);
                database.SaveChanges();
            }

            //Plane
            if (!database.Planes.Any())
            {
                    Airline southwest = database.Airlines.Where(a => a.AirlineName == "Southwest").FirstOrDefault();
                    Plane plane = new Plane("747", 2, southwest.AirlineID);
                    database.Planes.Add(plane);

                    plane = new Plane("747", 250, southwest.AirlineID);
                    database.Planes.Add(plane);

                    Airline delta = database.Airlines.Where(a => a.AirlineName == "Delta").FirstOrDefault();
                    plane = new Plane("737", 300, delta.AirlineID);
                    database.Planes.Add(plane);

                    database.SaveChanges();
            }

                if (!database.Flights.Any())
                {
                    Plane plane = database.Planes.FirstOrDefault();
                    Airport departure = database.Airports.Where(a => a.AirportName == "Pittsburgh").FirstOrDefault();
                    Airport arrival = database.Airports.Where(a => a.AirportName == "Orlando").FirstOrDefault();
                    Flight flight = new Flight(new DateTime(2024, 03, 01, 10, 00, 00), new DateTime(2024, 03, 01, 10, 30, 00), 100.00m, plane.PlaneID, departure.AirportID, arrival.AirportID);
                    database.Flights.Add(flight);

                    flight = new Flight(new DateTime(2024, 04, 01, 10, 30, 00), new DateTime(2024, 04, 01, 10, 30, 00), 200.00m, plane.PlaneID, arrival.AirportID, departure.AirportID);
                    database.Flights.Add(flight);

                    database.SaveChanges();
                }

            //Role Manager and User Manager
            RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<AppUser> userManager = 
                services.GetRequiredService<UserManager<AppUser>>();
            
            if (!database.Roles.Any())
            {
                IdentityRole role = new IdentityRole("Administrator");
                roleManager.CreateAsync(role).Wait();

                role = new IdentityRole("Customer");
                roleManager.CreateAsync(role).Wait();

                role = new IdentityRole("Employee");
                roleManager.CreateAsync(role).Wait();
            }

            if(!database.AppUsers.Any())
            {
                AppUser user = new AppUser("Test", "Administrator", new DateOnly(2000, 01, 01), "1234567890", "Test.Administrator1@test.com", "Test.Administrator1");
                userManager.CreateAsync(user).Wait();
                userManager.AddToRoleAsync(user, "Administrator").Wait();

                user = new AppUser("Test", "Employee", new DateOnly(2000, 01, 01), "1234567890", "Test.Employee1@test.com", "Test.Employee1");
                userManager.CreateAsync(user).Wait();
                userManager.AddToRoleAsync(user, "Employee").Wait();

                user = new AppUser("Test", "Customer", new DateOnly(2000, 01, 01), "1234567890", "Test.Customer1@test.com", "Test.Customer1");
                userManager.CreateAsync(user).Wait();
                userManager.AddToRoleAsync(user, "Customer").Wait();
            }

            if(!database.Tickets.Any())
            {
                Flight flight = database.Flights.FirstOrDefault();
                AppUser user = database.AppUsers.FirstOrDefault();
                Ticket ticket = new Ticket(flight.FlightID, user.Id, 100m);
                database.Tickets.Add(ticket);

                user = database.AppUsers.Where(a => a.Email == "Test.Employee1@test.com").FirstOrDefault();
                ticket = new Ticket(flight.FlightID, user.Id, 100m);
                database.Tickets.Add(ticket);

                database.SaveChanges();
            }
        }
    }
}
