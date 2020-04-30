using AutoMapper;
using SnowmanLabsChallenge.Application.AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SnowmanLabsChallenge.Application.Tests
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
    }
}
