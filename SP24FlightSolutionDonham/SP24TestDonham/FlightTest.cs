using Moq;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Controllers;
using SP24MVCDonham.Models;
using SP24MVCDonham.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP24TestDonham
{
    public class FlightTest
    {
        //TDD - Test Driven Development
        //RGR - Red Green Refactor
        //Red - when first write test
        //Green - get it to pass
        //Refactor - putting final touches on it
        private Mock<IFlightRepo> mockFlightRepo;
        private Mock<IAirportRepo> mockAirportRepo;
        private Mock<IAirlineRepo> mockAirlineRepo;
        private FlightController controller;

        public FlightTest()
        {
            this.mockFlightRepo = new Mock<IFlightRepo>();
            this.mockAirportRepo = new Mock<IAirportRepo>();
            this.mockAirlineRepo = new Mock<IAirlineRepo>();
            this.controller = new FlightController
                (this.mockFlightRepo.Object, this.mockAirportRepo.Object, this.mockAirlineRepo.Object);
        }

        [Fact]
        public void ShouldSearchFlightsByDepartureAriportID()
        {
            //AAA
            //Arrange
            SearchFlightsViewModel viewModel = new SearchFlightsViewModel();
            viewModel.DepartureAirportID = 1;
            int expected = 1;
            this.mockFlightRepo.Setup(m => m.ListAllFlights()).Returns(CreateMockFlights());
            this.mockAirportRepo.Setup(m => m.ListAllAirports()).Returns(new List<Airport>());
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns(new List<Airline>());

            //Act
            this.controller.SearchFlights(viewModel);

            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);
        }

        [Fact]
        public void ShouldSearchFlightsByArrivalAriportID()
        {
            //AAA
            //Arrange
            SearchFlightsViewModel viewModel = new SearchFlightsViewModel();
            viewModel.ArrivalAirportID = 1;
            int expected = 1;
            this.mockFlightRepo.Setup(m => m.ListAllFlights()).Returns(CreateMockFlights());
            this.mockAirportRepo.Setup(m => m.ListAllAirports()).Returns(new List<Airport>());

            //Act
            this.controller.SearchFlights(viewModel);

            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);
        }

        [Fact]
        public void ShouldSearchFlightsByAirlineID()
        {
            //AAA
            //Arrange
            SearchFlightsViewModel viewModel = new SearchFlightsViewModel();
            viewModel.AirlineID = 1;
            int expected = 1;
            this.mockFlightRepo.Setup(m => m.ListAllFlights()).Returns(CreateMockFlights());
            this.mockAirportRepo.Setup(m => m.ListAllAirports()).Returns(new List<Airport>());

            //Act
            this.controller.SearchFlights(viewModel);

            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);
        }

        //HELPER METHODS - DRY
        public List<Flight> CreateMockFlights()
        {
            List<Flight> flights = new List<Flight>();

            Flight flight = new Flight(new DateTime(2024, 04, 10, 10, 00, 00), new DateTime(2024, 04, 10, 11, 00, 00),
                200m, 1, 1, 2);
            Plane plane = new Plane("737", 100, 1);
            flight.Plane = plane;
            flights.Add(flight);

            flight = new Flight(new DateTime(2024, 04, 10, 11, 00, 00), new DateTime(2024, 04, 10, 12, 00, 00),
                300m, 1, 2, 1);
            plane = new Plane("747", 200, 2);
            flight.Plane = plane;
            flights.Add(flight);

            return flights;
        }
    }
}
