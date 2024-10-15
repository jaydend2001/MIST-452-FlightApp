using Moq;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Controllers;
using SP24MVCDonham.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SP24TestDonham
{
    public class TicketTest
    {
        private Mock<IAppUserRepo> mockAppUserRepo;
        private Mock<IFlightRepo> mockFlightRepo;
        private Mock<ITicketRepo> mockTicketRepo;
        private TicketController controller;

        public TicketTest()
        {
            this.mockAppUserRepo = new Mock<IAppUserRepo>();
            this.mockFlightRepo = new Mock<IFlightRepo>();
            this.mockTicketRepo = new Mock<ITicketRepo>();
            this.controller = new TicketController
                (this.mockAppUserRepo.Object, this.mockFlightRepo.Object, this.mockTicketRepo.Object);
        }
        [Fact]
        public void ShouldPurchaseTicket()
        {
            //Arrange
            int flightID = 1;
            Flight flight = new Flight(new DateTime(2024, 10, 8), new DateTime(2024, 10, 9), 100m, 1, 1, 2);
            Plane plane = new Plane("737", 100, 1);
            flight.Plane = plane;
            this.mockFlightRepo.Setup(m => m.FindFlight(flightID)).Returns(flight);
            string appUSerID = "1";
            this.mockAppUserRepo.Setup(m => m.GetAppUserID()).Returns("1");
            Ticket ticket = null;
            this.mockTicketRepo.Setup(m => m.AddTicket(It.IsAny<Ticket>())).Returns(1).Callback<Ticket>(t => ticket = t);

            //Act
            this.controller.PurchaseTicket(flightID);

            //Assert
            this.mockTicketRepo.Verify(m => m.AddTicket(It.IsAny<Ticket>()), Times.Once);
            Assert.Equal(appUSerID, ticket.AppUserID);
            Assert.Equal(flight.Price, ticket.PurchasePrice);
            Assert.Equal(flightID, ticket.FlightID);
        }

        [Fact]
        public void ShouldNotPurchaseTicketBecauseFlightInProgress()
        {
            //Arrange
            int flightID = 1;
            Flight flight = new Flight(new DateTime(2024, 10, 8), new DateTime(2024, 10, 9), 100m, 1, 1, 2);
            flight.FlightStatus = FlightStatus.Departed;
            Plane plane = new Plane("737", 100, 1);
            flight.Plane = plane;
            this.mockFlightRepo.Setup(m => m.FindFlight(flightID)).Returns(flight);
            string appUSerID = "1";
            this.mockAppUserRepo.Setup(m => m.GetAppUserID()).Returns("1");

            //Act
            this.controller.PurchaseTicket(flightID);

            //Assert
            this.mockTicketRepo.Verify(m => m.AddTicket(It.IsAny<Ticket>()), Times.Never);
        }

        [Fact]
        public void ShouldNotPurchaseTicketBecauseFlightIsFull()
        {
            //Arrange
            int flightID = 1;
            Flight flight = new Flight(new DateTime(2024, 10, 8), new DateTime(2024, 10, 9), 100m, 1, 1, 2);
            Plane plane = new Plane("737", 1, 1);
            Ticket ticket = new Ticket(1, "1", 100m);
            flight.Tickets.Add(ticket);
            flight.Plane = plane;
            this.mockFlightRepo.Setup(m => m.FindFlight(flightID)).Returns(flight);
            string appUSerID = "1";
            this.mockAppUserRepo.Setup(m => m.GetAppUserID()).Returns("1");

            //Act
            this.controller.PurchaseTicket(flightID);

            //Assert
            this.mockTicketRepo.Verify(m => m.AddTicket(It.IsAny<Ticket>()), Times.Never);
        }
    }
}
