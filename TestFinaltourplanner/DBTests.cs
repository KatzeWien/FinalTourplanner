using FinalTourplanner.BL;
using FinalTourplanner.DL;
using FinalTourplanner.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFinaltourplanner
{
    public class DBTests
    {
        [Test]
        public void TestAddTour()
        {
            var mockSet = new Mock<DbSet<Tour>>();
            var mockContext = new Mock<MyDBContext>(new DbContextOptions<MyDBContext>());
            mockContext.Setup(c => c.Tour).Returns(mockSet.Object);
            mockContext.Setup(c => c.SaveChanges()).Returns(1);

            var service = new TourRepository(mockContext.Object);
            var tour = new Tour { Name = "UnitTestTour" };

            service.AddTour(tour);

            mockSet.Verify(s => s.Add(It.Is<Tour>(t => t.Name == "UnitTestTour")), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public void TestAddTourLog()
        {
            var mockSet = new Mock<DbSet<TourLog>>();
            var mockContext = new Mock<MyDBContext>(new DbContextOptions<MyDBContext>());
            mockContext.Setup(c => c.TourLog).Returns(mockSet.Object);
            mockContext.Setup(c => c.SaveChanges()).Returns(1);

            var service = new TourLogRepository(mockContext.Object);
            var tourLog = new TourLog { NameOfTour = "UnitTestTour" };

            service.AddTourLog(tourLog);

            mockSet.Verify(s => s.Add(It.Is<TourLog>(t => t.NameOfTour == "UnitTestTour")), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
