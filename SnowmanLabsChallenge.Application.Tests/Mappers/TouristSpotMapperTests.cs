using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnowmanLabsChallenge.Application.ViewModels;
using SnowmanLabsChallenge.Domain.Models;
using System;

namespace SnowmanLabsChallenge.Application.Tests.Mappers
{
    [TestClass]
    public class TouristSpotMapperTests : BaseMapperUnitTest
    {
        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        public override void DomainToViewModelTest()
        {
            var rnd = new Random();

            var id = rnd.Next();
            var uuid = Guid.NewGuid();
            var createdOn = DateTime.UtcNow;
            var active = true;
            var ownerId = Guid.NewGuid();
            var name = "TouristSpot 12";
            var categoryId = 10;
            var latitude = 80.3553;
            var longitude = -139.2232;

            var model = new TouristSpot(id, uuid, createdOn, active, ownerId, name, categoryId, latitude, longitude);
            var viewModel = this._mapper.Map<TouristSpot, TouristSpotViewModel>(model);

            Type type = typeof(TouristSpot);
            var totalProperties = base.TotalProperties(type);
            var testedProperties = 0;

            Assert.AreEqual(model.Id, viewModel.Id);
            testedProperties++;

            Assert.AreEqual(model.Uuid, viewModel.Uuid);
            testedProperties++;

            Assert.AreEqual(model.CreatedOn, viewModel.CreatedOn);
            testedProperties++;

            Assert.AreEqual(model.Active, viewModel.Active);
            testedProperties++;

            Assert.AreEqual(model.OwnerId, viewModel.OwnerId);
            testedProperties++;

            Assert.AreEqual(model.Name, viewModel.Name);
            testedProperties++;

            Assert.AreEqual(model.CategoryId, viewModel.CategoryId);
            testedProperties++;

            Assert.AreEqual(model.Location.Y, viewModel.Latitude);
            testedProperties++;

            Assert.AreEqual(model.Location.X, viewModel.Longitude);
            testedProperties++;

            // If a new property is added, the test will fail to force the update of the test case
            Assert.AreEqual(testedProperties, totalProperties);
        }

        [TestMethod]
        [Owner("Vinicius Haninec Silva")]
        public override void ViewModelToDomainTest()
        {
            var rnd = new Random();

            var id = rnd.Next();
            var uuid = Guid.NewGuid();
            var createdOn = DateTime.UtcNow;
            var active = true;
            var ownerId = Guid.NewGuid();
            var name = "TouristSpot 12";
            var categoryId = 10;
            var latitude = 80.3553;
            var longitude = -139.2232;

            var viewModel = new TouristSpotViewModel {
                Id = id, 
                Uuid = uuid,
                CreatedOn = createdOn, 
                Active = active, 
                Name = name, 
                CategoryId = categoryId,
                Latitude = latitude,
                Longitude = longitude
            };
            var model = this._mapper.Map<TouristSpotViewModel, TouristSpot>(viewModel);

            Type type = typeof(TouristSpotViewModel);
            var totalProperties = base.TotalProperties(type);
            var testedProperties = 0;

            Assert.AreEqual(model.Id, viewModel.Id);
            testedProperties++;

            Assert.AreEqual(model.Uuid, viewModel.Uuid);
            testedProperties++;

            Assert.AreEqual(model.CreatedOn, viewModel.CreatedOn);
            testedProperties++;

            Assert.AreEqual(model.Active, viewModel.Active);
            testedProperties++;

            Assert.AreEqual(model.OwnerId, viewModel.OwnerId);
            testedProperties++;

            Assert.AreEqual(model.Name, viewModel.Name);
            testedProperties++;

            Assert.AreEqual(model.CategoryId, viewModel.CategoryId);
            testedProperties++;

            Assert.AreEqual(model.Location.Y, viewModel.Latitude);
            testedProperties++;

            Assert.AreEqual(model.Location.X, viewModel.Longitude);
            testedProperties++;

            // If a new property is added, the test will fail to force the update of the test case
            Assert.AreEqual(testedProperties, totalProperties);
        }
    }
}
