using Microsoft.Identity.Client;
using Moq;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Controllers;
using SP24MVCDonham.Models;
using SP24MVCDonham.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private Mock<IPlaneRepo> mockPlaneRepo;
        private Mock<IAppUserRepo> mockAppUserRepo;
        private FlightController controller;

        public FlightTest()
        {
            this.mockFlightRepo = new Mock<IFlightRepo>();
            this.mockAirportRepo = new Mock<IAirportRepo>();
            this.mockAirlineRepo = new Mock<IAirlineRepo>();
            this.mockPlaneRepo = new Mock<IPlaneRepo>();
            this.mockAppUserRepo = new Mock<IAppUserRepo>();
            this.controller = new FlightController (this.mockFlightRepo.Object, this.mockAirportRepo.Object, this.mockAirlineRepo.Object, 
            this.mockPlaneRepo.Object, this.mockAppUserRepo.Object);
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
            this.mockPlaneRepo.Setup(m => m.ListAllPlanes()).Returns(new List<Plane>());

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
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns(new List<Airline>());
            this.mockPlaneRepo.Setup(m => m.ListAllPlanes()).Returns(new List<Plane>());

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
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns(new List<Airline>());
            this.mockPlaneRepo.Setup(m => m.ListAllPlanes()).Returns(new List<Plane>());

            //Act
            this.controller.SearchFlights(viewModel);

            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);
        }

        [Fact]
        public void ShouldSearchFlightsByDepartureDateTimeAfterStartDate()
        {
            //AAA
            //Arrange
            SearchFlightsViewModel viewModel = new SearchFlightsViewModel();
            viewModel.StartDate = new DateTime(2024, 05, 01, 00, 00, 00);
            int expected = 1;

            this.mockFlightRepo.Setup(m => m.ListAllFlights()).Returns(CreateMockFlights());
            this.mockAirportRepo.Setup(m => m.ListAllAirports()).Returns(new List<Airport>());
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns(new List<Airline>());
            this.mockPlaneRepo.Setup(m => m.ListAllPlanes()).Returns(new List<Plane>());

            //Act
            this.controller.SearchFlights(viewModel);

            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);
        }

        [Fact]
        public void ShouldSearchFlightsByDepartureDateTimeBeforeEndDate()
        {
            //AAA
            //Arrange
            SearchFlightsViewModel viewModel = new SearchFlightsViewModel();
            viewModel.EndDate = new DateTime(2024, 05, 01, 00, 00, 00);
            int expected = 1;

            this.mockFlightRepo.Setup(m => m.ListAllFlights()).Returns(CreateMockFlights());
            this.mockAirportRepo.Setup(m => m.ListAllAirports()).Returns(new List<Airport>());
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns(new List<Airline>());
            this.mockPlaneRepo.Setup(m => m.ListAllPlanes()).Returns(new List<Plane>());

            //Act
            this.controller.SearchFlights(viewModel);

            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);
        }

        [Fact]
        public void ShouldSearchFlightsByAirlineName()
        { 
            //Arrange
            SearchFlightsViewModel viewModel = new SearchFlightsViewModel();
            viewModel.AirlineName = "DeLt";
            int expected = 1;

            this.mockFlightRepo.Setup(m => m.ListAllFlights()).Returns(CreateMockFlights());
            this.mockAirportRepo.Setup(m => m.ListAllAirports()).Returns(new List<Airport>());
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns(new List<Airline>());
            this.mockPlaneRepo.Setup(m => m.ListAllPlanes()).Returns(new List<Plane>());

            //Act
            this.controller.SearchFlights(viewModel);

            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);
        }

        [Fact]
        public void ShouldSearchFlightsWithAvailableSeats()
        {
            //AAA
            //Arrange
            SearchFlightsViewModel viewModel = new SearchFlightsViewModel();
            viewModel.AvailableFlights = true;
            int expected = 1;

            this.mockFlightRepo.Setup(m => m.ListAllFlights()).Returns(CreateMockFlights());
            this.mockAirportRepo.Setup(m => m.ListAllAirports()).Returns(new List<Airport>());
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns(new List<Airline>());
            this.mockPlaneRepo.Setup(m => m.ListAllPlanes()).Returns(new List<Plane>());

            //Act
            this.controller.SearchFlights(viewModel);

            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);
        }

        [Fact]
        public void ShouldSearchFlightsByFlightStatus()
        {
            //AAA
            //Arrange
            SearchFlightsViewModel viewModel = new SearchFlightsViewModel();
            viewModel.FlightStatus = FlightStatus.Planned;
            int expected = 1;

            this.mockFlightRepo.Setup(m => m.ListAllFlights()).Returns(CreateMockFlights());
            this.mockAirportRepo.Setup(m => m.ListAllAirports()).Returns(new List<Airport>());
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns(new List<Airline>());
            this.mockPlaneRepo.Setup(m => m.ListAllPlanes()).Returns(new List<Plane>());

            //Act
            this.controller.SearchFlights(viewModel);

            //Assert
            Assert.Equal(expected, viewModel.SearchResult.Count);
        }

        public List<Flight> CreateMockFlights()
        {
            List<Flight> flights = new List<Flight>();

            Flight flight = new Flight(new DateTime(2024, 04, 10, 10, 00, 00), new DateTime(2024, 04, 10, 11, 00, 00), 200m, 1, 2, 2);
            Plane plane = new Plane("737", 1, 1);
            Airline airline = new Airline("Southwest");
            plane.Airline = airline;
            flight.Plane = plane;
            flight.Tickets.Add(new Ticket(1, "fhdfhjsd", 100));
            flight.FlightID = 1;
            flights.Add(flight);

            flight = new Flight(new DateTime(2024, 05, 10, 11, 00, 00), new DateTime(2024, 04, 10, 12, 00, 00), 300m, 2, 1, 1);
            plane = new Plane("747", 200, 2);
            airline = new Airline("Delta");
            plane.Airline = airline;
            flight.Plane = plane;
            flight.FlightStatus = FlightStatus.Cancelled;
            flights.Add(flight);

            return flights;
        }

        [Fact]
        public void ShouldAddFlight()
        {
            //Arrange
            FlightViewModel viewModel = new FlightViewModel();
            viewModel.DepartureAirportID = 1;
            viewModel.ArrivalAirportID = 2;
            viewModel.DepartureDateTime = new DateTime(2024, 10, 10, 9, 00, 00);
            viewModel.ArrivalDateTime = new DateTime(2024, 10, 10, 10, 0, 0);
            viewModel.Price = 100m;
            viewModel.PlaneID = 3;

            this.mockFlightRepo.Setup(m => m.ListAllFlights()).Returns(CreateMockFlights());
            this.mockAirportRepo.Setup(m => m.ListAllAirports()).Returns(new List<Airport>());
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns(new List<Airline>());
            this.mockPlaneRepo.Setup(m => m.ListAllPlanes()).Returns(new List<Plane>());
            Flight newFlight = null;
            this.mockFlightRepo.Setup(m => m.AddFlight(It.IsAny<Flight>())).Returns(1).Callback<Flight>(f => newFlight = f);

            //Act
            this.controller.AddFlight(viewModel);

            //Assert
            this.mockFlightRepo.Verify(m => m.AddFlight(It.IsAny<Flight>()), Times.Once());
            Assert.Equal(viewModel.DepartureAirportID, newFlight.DepartureAirportID);
            Assert.Equal(viewModel.ArrivalAirportID, newFlight.ArrivalAirportID);
            Assert.Equal(viewModel.PlaneID, newFlight.PlaneID);
            Assert.Equal(viewModel.DepartureDateTime, newFlight.DepartureDateTime);
            Assert.Equal(viewModel.ArrivalDateTime, newFlight.EstimatedArrivalDateTime);
            Assert.Equal(viewModel.Price, newFlight.Price);
        }

        [Fact]
        public void ShouldNotAddFlightBecauseDuplicate()
        {
            //Arrange
            FlightViewModel viewModel = new FlightViewModel();
            viewModel.DepartureAirportID = 1;
            viewModel.ArrivalAirportID = 2;
            viewModel.DepartureDateTime = new DateTime(2024, 04, 10, 10, 0, 0);
            viewModel.ArrivalDateTime = new DateTime(2024, 10, 10, 11, 0, 0);
            viewModel.Price = 100m;
            viewModel.PlaneID = 1;

            this.mockFlightRepo.Setup(m => m.ListAllFlights()).Returns(CreateMockFlights());
            this.mockAirportRepo.Setup(m => m.ListAllAirports()).Returns(new List<Airport>());
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns(new List<Airline>());
            this.mockPlaneRepo.Setup(m => m.ListAllPlanes()).Returns(new List<Plane>());

            //Act
            this.controller.AddFlight(viewModel);

            //Assert
            this.mockFlightRepo.Verify(m => m.AddFlight(It.IsAny<Flight>()), Times.Never());
        }

        [Fact]
        public void ShouldNotEditFlightBecauseDuplicate()
        {
            //Arrange
            FlightViewModel viewModel = new FlightViewModel();
            viewModel.DepartureAirportID = 1;
            viewModel.ArrivalAirportID = 2;
            viewModel.DepartureDateTime = new DateTime(2024, 04, 10, 10, 00, 00);
            viewModel.ArrivalDateTime = new DateTime(2024, 10, 10, 11, 0, 0);
            viewModel.Price = 100m;
            viewModel.PlaneID = 1;
            viewModel.FlightID = 2;

            this.mockFlightRepo.Setup(m => m.ListAllFlights()).Returns(CreateMockFlights());
            this.mockAirportRepo.Setup(m => m.ListAllAirports()).Returns(new List<Airport>());
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns(new List<Airline>());
            this.mockPlaneRepo.Setup(m => m.ListAllPlanes()).Returns(new List<Plane>());

            //Act
            this.controller.EditFlight(viewModel);

            //Assert
            this.mockFlightRepo.Verify(m => m.EditFlight(It.IsAny<Flight>()), Times.Never());
        }

        [Fact]
        public void ShouldNotAddEditFlightBecauseModelInvalid()
        {
            //Arrange
            FlightViewModel viewModel = new FlightViewModel();
            viewModel.ArrivalAirportID = 2;
            viewModel.DepartureDateTime = new DateTime(2024, 04, 10, 10, 0, 0);
            viewModel.ArrivalDateTime = new DateTime(2024, 10, 10, 10, 0, 0);
            viewModel.Price = 100m;
            viewModel.PlaneID = 1;
            string expectedError = "The DepartureAirportID field is required.";

            //Act
            //this.controller.AddFlight(viewModel);
            List<ValidationResult> result = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel),
                result);

            //Assert
            Assert.False(valid);
            Assert.Equal(expectedError, result[0].ErrorMessage);
        }

        [Fact]
        public void ShouldEditFlight()
        {
            //Arrange
            FlightViewModel viewModel = new FlightViewModel();
            viewModel.DepartureAirportID = 1;
            viewModel.ArrivalAirportID = 2;
            viewModel.DepartureDateTime = new DateTime(2024, 10, 10, 9, 0, 0);
            viewModel.ArrivalDateTime = new DateTime(2024, 10, 10, 10, 0, 0);
            viewModel.Price = 100m;
            viewModel.PlaneID = 3;
            viewModel.FlightID = 1;

            this.mockFlightRepo.Setup(m => m.ListAllFlights()).Returns(CreateMockFlights());
            this.mockAirportRepo.Setup(m => m.ListAllAirports()).Returns(new List<Airport>());
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns(new List<Airline>());
            this.mockPlaneRepo.Setup(m => m.ListAllPlanes()).Returns(new List<Plane>());

            Flight oldFlight = new Flight(new DateTime(2024, 9, 9, 10, 0, 0), new DateTime(2024, 9, 9, 11, 0, 0), 10m, 100, 100, 200);
            oldFlight.FlightID = 1;
            this.mockFlightRepo.Setup(m => m.FindFlight(oldFlight.FlightID)).Returns(oldFlight);

            //Act
            this.controller.EditFlight(viewModel);

            //Assert
            this.mockFlightRepo.Verify(m => m.EditFlight(It.IsAny<Flight>()), Times.Once);
            //Assert.NotNull(newFlight);
            Assert.Equal(viewModel.DepartureAirportID, oldFlight.DepartureAirportID);
            Assert.Equal(viewModel.ArrivalAirportID, oldFlight.ArrivalAirportID);
            Assert.Equal(viewModel.PlaneID, oldFlight.PlaneID);
            Assert.Equal(viewModel.DepartureDateTime, oldFlight.DepartureDateTime);
            Assert.Equal(viewModel.ArrivalDateTime, oldFlight.EstimatedArrivalDateTime);
            Assert.Equal(viewModel.Price, oldFlight.Price);
        }

        [Fact]
        public void ShouldDeleteFlight()
        {
            //Arrange
            Flight flight = new Flight(new DateTime(2024, 9, 9, 10, 0, 0), new DateTime(2024, 9, 9, 11, 0, 0), 10m, 1, 1, 2);
            this.mockFlightRepo.Setup(m => m.FindFlight (flight.FlightID)).Returns(flight);
            int flightID = 1;

            //Act
            this.controller.DeleteFlight(flight.FlightID);

            //Assert
            this.mockFlightRepo.Verify(m => m.DeleteFlight(flight), Times.Once);
        }

        [Fact]
        public void ShouldNotDeleteFlightBecauseTicketsAreSold()
        {
            //Arrange
            Flight flight = new Flight(new DateTime(2024, 9, 9, 10, 0, 0), new DateTime(2024, 9, 9, 11, 0, 0), 10m, 100, 100, 200);
            flight.FlightID = 1;
            flight.Tickets.Add(new Ticket());

            this.mockFlightRepo.Setup(m => m.FindFlight(flight.FlightID)).Returns(flight);
            this.mockAirportRepo.Setup(m => m.ListAllAirports()).Returns(new List<Airport>());
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns(new List<Airline>());
            this.mockPlaneRepo.Setup(m => m.ListAllPlanes()).Returns(new List<Plane>());

            //Act
            this.controller.DeleteFlight(flight.FlightID);

            //Assert
            this.mockFlightRepo.Verify(m => m.DeleteFlight(flight), Times.Never);
        }

        [Fact]
        public void ShouldNotDeleteFlightBecauseFlightInProgress()
        {
            //Arrange
            Flight flight = new Flight(new DateTime(2024, 9, 9, 10, 0, 0), new DateTime(2024, 9, 9, 11, 0, 0), 10m, 100, 100, 200);
            flight.FlightID = 1;
            flight.FlightStatus = FlightStatus.Delayed;

            this.mockFlightRepo.Setup(m => m.FindFlight(flight.FlightID)).Returns(flight);
            this.mockAirportRepo.Setup(m => m.ListAllAirports()).Returns(new List<Airport>());
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns(new List<Airline>());
            this.mockPlaneRepo.Setup(m => m.ListAllPlanes()).Returns(new List<Plane>());

            //Act
            this.controller.DeleteFlight(flight.FlightID);

            //Assert
            this.mockFlightRepo.Verify(m => m.DeleteFlight(flight), Times.Never);
        }

        [Fact]
        public void ShouldEditFlightStatus()
        {
            //Arrange
            int flightID = 1;
            FlightStatus status = FlightStatus.Departed;
            Flight flight = new Flight(new DateTime(2024, 9, 9, 10, 0, 0), new DateTime(2024, 9, 9, 11, 0, 0), 10m, 100, 100, 200);
            this.mockFlightRepo.Setup(m => m.FindFlight(flightID)).Returns(flight);

            //Act
            this.controller.EditFlightStatus(flightID, status);

            //Assert
            this.mockFlightRepo.Verify(m => m.EditFlight(flight), Times.Once);
            Assert.Equal(status, flight.FlightStatus);
        }
    }
}
