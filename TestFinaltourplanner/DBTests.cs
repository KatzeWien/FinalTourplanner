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







        
        // tests using EF InMemory DB
        

        private MyDBContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<MyDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // unique db per test
                .Options;
            return new MyDBContext(options);
        }

        [Test]
        public void Test_01_Add_Tour_Works()
        {
            using var ctx = CreateInMemoryContext();
            var repo = new TourRepository(ctx);

            var tour = new Tour("Alpen", "Schöne Tour", "Wien", "Graz", "car", 100, TimeSpan.FromHours(2));
            repo.AddTour(tour);

            var all = repo.GetAllTours();
            Assert.That(all.Count, Is.EqualTo(1));
            Assert.That(all.First().Name, Is.EqualTo("Alpen"));
        }

        [Test]
        public void Test_02_Edit_Tour_Works()
        {
            using var ctx = CreateInMemoryContext();
            var repo = new TourRepository(ctx);

            var tour = new Tour("Alpen", "Schöne Tour", "Wien", "Graz", "car", 100, TimeSpan.FromHours(2));
            repo.AddTour(tour);

            tour.Description = "Neue Beschreibung";
            repo.UpdateTour(tour, "Alpen");

            var updated = repo.GetSpecificTour("Alpen");
            Assert.That(updated.Description, Is.EqualTo("Neue Beschreibung"));
        }

        [Test]
        public void Test_03_Remove_Tour_Works()
        {
            using var ctx = CreateInMemoryContext();
            var repo = new TourRepository(ctx);

            var tour = new Tour("Alpen", "Schöne Tour", "Wien", "Graz", "car", 100, TimeSpan.FromHours(2));
            repo.AddTour(tour);

            repo.DeleteTour("Alpen");

            var all = repo.GetAllTours();
            Assert.That(all, Is.Empty);
        }

        [Test]
        public void Test_04_Add_TourLog_Works()
        {
            using var ctx = CreateInMemoryContext();
            var repo = new TourLogRepository(ctx);

            var log = new TourLog("Alpen", DateTime.UtcNow, "Super!", "2", 50, TimeSpan.FromHours(1), "5");
            repo.AddTourLog(log);

            var allLogs = repo.GetAllTourLogs();
            Assert.That(allLogs.Count, Is.EqualTo(1));
            Assert.That(allLogs.First().Comment, Is.EqualTo("Super!"));
        }

        [Test]
        public void Test_GetAllTours_Returns_All()
        {
            using var ctx = CreateInMemoryContext();
            var repo = new TourRepository(ctx);

            var t1 = new Tour("Alpen", "Schöne Tour", "Wien", "Graz", "car", 100, TimeSpan.FromHours(2));
            var t2 = new Tour("Berge", "Tolle Tour", "Linz", "Salzburg", "bike", 80, TimeSpan.FromHours(4));
            repo.AddTour(t1);
            repo.AddTour(t2);

            var all = repo.GetAllTours();
            Assert.That(all.Count, Is.EqualTo(2));
            Assert.That(all.Select(x => x.Name), Does.Contain("Alpen").And.Contain("Berge"));
        }


        [Test]
        public void RemoveTourLogWorks()
        {
            using var ctx = CreateInMemoryContext();
            var repo = new TourLogRepository(ctx);

            // add log with a known Id so DeleteTourLog can find it
            var log = new TourLog
            {
                Id = 1,
                NameOfTour = "Alpen",
                DateInput = DateTime.UtcNow,
                Comment = "to be removed",
                Difficulty = "2",
                Distance = 10,
                TotalTime = TimeSpan.FromMinutes(30),
                Rating = "4"
            };
            repo.AddTourLog(log);

            



            repo.DeleteTourLog(1);

            
            var all = repo.GetAllTourLogs();
            Assert.That(all, Is.Empty);


        }

        [Test]
        public void EditTourLogWorks()
        {
            using var ctx = CreateInMemoryContext();
            var repo = new TourLogRepository(ctx);


            // original row
            var original = new TourLog
            {
                Id = 42,
                NameOfTour = "Alpen",
                DateInput = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Comment = "old",
                Difficulty = "1",
                Distance = 5,
                TotalTime = TimeSpan.FromMinutes(20),
                Rating = "3"
            };


            repo.AddTourLog(original);

            // Prepare updated values (UpdateTourLog copies)
            var updated = new TourLog
            {
                // Id manual set for secure
                
                Id = 999,
                NameOfTour = "Alpen", 
                DateInput = new DateTime(2024, 2, 2, 0, 0, 0, DateTimeKind.Utc),
                Comment = "new",
                Difficulty = "2",
                Distance = 12,
                TotalTime = TimeSpan.FromMinutes(45),
                Rating = "5"
            };



            
            repo.UpdateTourLog(updated, 42);

            
            var row = repo.GetAllTourLogs().Single(); 
            Assert.That(row.Id, Is.EqualTo(42)); // id change
            Assert.That(row.DateInput, Is.EqualTo(new DateTime(2024, 2, 2, 0, 0, 0, DateTimeKind.Utc)));
            Assert.That(row.Comment, Is.EqualTo("new"));
            Assert.That(row.Difficulty, Is.EqualTo("2"));
            Assert.That(row.Distance, Is.EqualTo(12));
            Assert.That(row.TotalTime, Is.EqualTo(TimeSpan.FromMinutes(45)));
            Assert.That(row.Rating, Is.EqualTo("5"));
        }

    }
}

