using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnowmanLabsChallenge.Domain.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowmanLabsChallenge.Domain.Tests.Models
{
    [TestClass]
    public class TouristSpotTests
    {
        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        public void ConstructorTest()
        {
            var rnd = new Random();
            var length = 10000;
            var tests = length / 10; // 10% of tests

            var namesConcurrentBag = new ConcurrentBag<string>();
            List<Task> namesAddTasks = new List<Task>();
            for (int index = 0; index < length; index++)
            {
                namesAddTasks.Add(Task.Run(() => namesConcurrentBag.Add($"Tourist Spot {index}")));
            }

            Task.WaitAll(namesAddTasks.ToArray());
            var nomes = namesConcurrentBag.ToList();

            Parallel.For(0, tests, index =>
            {
                var id = index + 2;
                var uuid = Guid.NewGuid();
                var createdOn = DateTime.UtcNow;
                var active = index % 2 == 0 ? true : false;

                var ownerId = Guid.NewGuid();
                var nameIndex = rnd.Next(0, length);
                var name = nomes[nameIndex];
                var categoryId = index + 10;
                var latitude = rnd.Next(-90, 90);
                var longitude = rnd.Next(-180, 180);

                var touristSpot = new TouristSpot(
                    id, 
                    uuid, 
                    createdOn, 
                    active, 
                    ownerId, 
                    name, 
                    categoryId, 
                    latitude, 
                    longitude
                );

                Assert.AreEqual(touristSpot.Id, id);
                Assert.AreEqual(touristSpot.Uuid, uuid);
                Assert.AreEqual(touristSpot.CreatedOn, createdOn);
                Assert.AreEqual(touristSpot.Active, active);
                Assert.AreEqual(touristSpot.OwnerId, ownerId);
                Assert.AreEqual(touristSpot.Name, name);
                Assert.AreEqual(touristSpot.CategoryId, categoryId);
                Assert.AreEqual(touristSpot.Location.Y, latitude);
                Assert.AreEqual(touristSpot.Location.X, longitude);
            });
        }

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        [ExpectedException(typeof(SnowmanLabsChallengeException))]
        public void NameNull()
        {
            var rnd = new Random();

            var ownerId = Guid.NewGuid();
            string name = null;
            var categoryId = 10;
            var latitude = rnd.Next(-90, 90);
            var longitude = rnd.Next(-180, 180);

            new TouristSpot(1, Guid.NewGuid(), DateTime.UtcNow, true, ownerId, name, categoryId, latitude, longitude);
        }

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        [ExpectedException(typeof(SnowmanLabsChallengeException))]
        public void NameEmpty()
        {
            var rnd = new Random();

            var ownerId = Guid.NewGuid();
            var name = string.Empty;
            var categoryId = 10;
            var latitude = rnd.Next(-90, 90);
            var longitude = rnd.Next(-180, 180);

            new TouristSpot(1, Guid.NewGuid(), DateTime.UtcNow, true, ownerId, name, categoryId, latitude, longitude);
        }

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        [ExpectedException(typeof(SnowmanLabsChallengeException))]
        public void NameMoreThan255Characters()
        {
            var rnd = new Random();

            var ownerId = Guid.NewGuid();
            var name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent rutrum augue ac sagittis mattis. Suspendisse aliquet sit amet nibh ut dignissim. Cras tincidunt enim lobortis sapien tincidunt dictum. Suspendisse fringilla dictum ligula porta hendrerit. Nullam a ex sit amet felis sodales accumsan.";
            var categoryId = 10;
            var latitude = rnd.Next(-90, 90);
            var longitude = rnd.Next(-180, 180);

            new TouristSpot(1, Guid.NewGuid(), DateTime.UtcNow, true, ownerId, name, categoryId, latitude, longitude);
        }

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        [ExpectedException(typeof(SnowmanLabsChallengeException))]
        public void CategoryIdZero()
        {
            var rnd = new Random();

            var ownerId = Guid.NewGuid();
            var name = "Tourist Spot 12";
            var categoryId = 0;
            var latitude = rnd.Next(-90, 90);
            var longitude = rnd.Next(-180, 180);

            new TouristSpot(1, Guid.NewGuid(), DateTime.UtcNow, true, ownerId, name, categoryId, latitude, longitude);
        }

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        [ExpectedException(typeof(SnowmanLabsChallengeException))]
        public void LatitudeLessThanMinus90()
        {
            var ownerId = Guid.NewGuid();
            var name = "Tourist Spot 12";
            var categoryId = 10;
            var latitude = -90.1;
            var longitude = 0;

            new TouristSpot(1, Guid.NewGuid(), DateTime.UtcNow, true, ownerId, name, categoryId, latitude, longitude);
        }

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        [ExpectedException(typeof(SnowmanLabsChallengeException))]
        public void LatitudeGreaterThan90()
        {
            var ownerId = Guid.NewGuid();
            var name = "Tourist Spot 12";
            var categoryId = 10;
            var latitude = 90.1;
            var longitude = 0;

            new TouristSpot(1, Guid.NewGuid(), DateTime.UtcNow, true, ownerId, name, categoryId, latitude, longitude);
        }

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        [ExpectedException(typeof(SnowmanLabsChallengeException))]
        public void LongitudeLessThanMinus180()
        {
            var ownerId = Guid.NewGuid();
            var name = "Tourist Spot 12";
            var categoryId = 10;
            var latitude = 0;
            var longitude = -180.1;

            new TouristSpot(1, Guid.NewGuid(), DateTime.UtcNow, true, ownerId, name, categoryId, latitude, longitude);
        }

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        [ExpectedException(typeof(SnowmanLabsChallengeException))]
        public void LongitudeGreaterThan180()
        {
            var ownerId = Guid.NewGuid();
            var name = "Tourist Spot 12";
            var categoryId = 10;
            var latitude = 0;
            var longitude = 180.1;

            new TouristSpot(1, Guid.NewGuid(), DateTime.UtcNow, true, ownerId, name, categoryId, latitude, longitude);
        }
    }
}
