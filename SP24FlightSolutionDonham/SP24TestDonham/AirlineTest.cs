using SP24MVCDonham.Models;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace SP24TestDonham
{
    public class AirlineTest
    {
        private Mock<IAirlineRepo> mockAirlineRepo;
        private AirlineController controller;

        public AirlineTest()
        {
            this.mockAirlineRepo = new Mock<IAirlineRepo>();
            this.controller = new AirlineController(this.mockAirlineRepo.Object);
        }

        //TDD - Test Driven Development
        [Fact]
        public void ShouldListAllAirlines()
        {
            //AAA Approach
            //Arrange - Setup
            List<Airline> airlines = CreateMockAirlines()
            this.mockAirlineRepo.Setup(m => m.ListAllAirlines()).Returns
                (airlines);

            //Act - Calling the functionality
            ViewResult result = this.controller.ListAllAirlines() as ViewResult;
            List<Airline> resultAirlines = result.Model as List<Airline>;

            //Assert - Compare Expected vs Actual
            Assert.Equal(airlines.Count, resultAirlines.Count);
        }

        public List<Airline> CreateMockAirlines()
        {
            List<Airline> airlines = new List<Airline>();

            Airline airline = new Airline("Test Airline 1");
            airlines.Add(airline);

            airline = new Airline("Test Airline 2");
            airlines.Add(airline);

            return airlines;
        }
    }
}
