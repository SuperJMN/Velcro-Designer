using System.Collections.Generic;
using AutoMapper;
using DynamicData;
using Grace.DependencyInjection;
using Project = Designer.Domain.Models.Project;

namespace Designer.Core.Mapper
{
    public class ProjectMapper : IProjectMapper
    {
        private readonly IMapper mapper;

        public ProjectMapper(ILocatorService locator)
        {
            mapper = new MapperConfiguration(config =>
            {
                config.CreateMap(typeof(IEnumerable<>), typeof(SourceList<>)).ConvertUsing(typeof(EnumerableToSourceListConverter<,>));
                config.CreateMap(typeof(SourceList<>), typeof(IEnumerable<>)).ConvertUsing(typeof(SourceListToEnumerableConverter<,>));

                config.ConstructServicesUsing(locator.Locate);
                config.AddProfile<ModelToViewModelProfile>();
                config.AddProfile<ViewModelToModelProfile>();
            }).CreateMapper();
        }

        public Project Map(Domain.ViewModels.Project project)
        {
            return mapper.Map<Project>(project);
        }

        public Domain.ViewModels.Project Map(Project project)
        {
            return mapper.Map<Domain.ViewModels.Project>(project);
        }
    }
}