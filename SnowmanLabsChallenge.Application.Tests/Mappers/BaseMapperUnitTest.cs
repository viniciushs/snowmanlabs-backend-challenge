using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetTopologySuite.Geometries;
using SnowmanLabsChallenge.Application.AutoMapper;
using System;
using System.Linq;

namespace SnowmanLabsChallenge.Application.Tests.Mappers
{
    [TestClass]
    public abstract class BaseMapperUnitTest
    {
        protected readonly IMapper _mapper;

        public BaseMapperUnitTest()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new DomainToViewModelMappingProfile());
                opts.AddProfile(new ViewModelToDomainMappingProfile());
            });

            this._mapper = config.CreateMapper();
        }

        public abstract void DomainToViewModelTest();

        public abstract void ViewModelToDomainTest();

        public virtual int TotalProperties(Type type)
        {
            var totalProperties = type.GetProperties().Count();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var propertyType = property.PropertyType;
                var propertyTypeCode = Type.GetTypeCode(propertyType);
                if (propertyTypeCode == TypeCode.Object)
                {
                    if (propertyType != typeof(Guid) && propertyType != typeof(Point) && !propertyType.FullName.Contains("System.Nullable"))
                    {
                        totalProperties--;
                    }

                    // We need to add +1 because a point type has X and Y and we counted only 1 of then.
                    if (propertyType == typeof(Point))
                    {
                        totalProperties++;
                    }
                }
            }

            return totalProperties;
        }
    }
}
