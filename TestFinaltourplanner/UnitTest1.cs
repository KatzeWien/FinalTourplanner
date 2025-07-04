using FinalTourplanner.Models;
using FinalTourplanner.ViewModel;
using NUnit.Framework;

namespace TestFinaltourplanner
{
    public class Tests
    {
        private AllDataManagement dataManagement;
        [SetUp]
        public void Setup()
        {
            dataManagement = new AllDataManagement();
        }

        [Test]
        public void AddTour()
        {
            int beforeAdd = dataManagement.Tours.Count;
            Tour tour = new Tour("name", "description", "from", "to", "transport", 3, new TimeSpan(12, 00, 00));
            dataManagement.AddTour(tour);
            int afterAdd = dataManagement.Tours.Count;
            //Assert.IsTrue(afterAdd == beforeAdd + 1);
            Assert.That(afterAdd, Is.EqualTo(beforeAdd + 1));
        }

        [Test]
        public void AddTourLog()
        {
            int beforeAdd = dataManagement.Logs.Count;
            TourLog tourLog = new TourLog("name", new DateTime(2024, 10, 27, 12, 00, 00), "comment", "difficulty", 5, new TimeSpan(12, 00, 00), "rating");
            dataManagement.AddTourLog(tourLog);
            int afterAdd = dataManagement.Logs.Count;
            //Assert.IsTrue(afterAdd == beforeAdd + 1);
            Assert.That(afterAdd, Is.EqualTo(beforeAdd + 1));
        }

        [Test]
        public void DeleteTour()
        {
            Tour tour = new Tour("name", "description", "from", "to", "transport", 3, new TimeSpan(12, 00, 00));
            dataManagement.AddTour(tour);
            int beforeDel = dataManagement.Tours.Count;
            dataManagement.DeleteTour(tour);
            int afterDel = dataManagement.Tours.Count;
            //Assert.IsTrue(afterDel == beforeDel - 1);
            Assert.That(afterDel, Is.EqualTo(beforeDel - 1));

        }

        [Test]
        public void DeleteTourLog()
        {
            TourLog tourLog = new TourLog("name", new DateTime(2024, 10, 27, 12, 00, 00), "comment", "difficulty", 5, new TimeSpan(12, 00, 00), "rating");
            dataManagement.AddTourLog(tourLog);
            int beforeDel = dataManagement.Logs.Count;
            dataManagement.DeleteTourLogBasedOnIndex(0);
            int afterDel = dataManagement.Logs.Count;
            //Assert.IsTrue(afterDel == beforeDel - 1);
            Assert.That(afterDel, Is.EqualTo(beforeDel - 1));
        }
    }
}