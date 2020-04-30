﻿using AutoMapper;
using SnowmanLabsChallenge.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SnowmanLabsChallenge.WebApi.Configurations
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(
                typeof(DomainToViewModelMappingProfile),
                typeof(ViewModelToDomainMappingProfile)
            );
        }
    }
}
