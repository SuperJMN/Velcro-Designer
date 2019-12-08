using AutoMapper;
using Designer.Domain.Models;
using Grace.DependencyInjection;

namespace Designer.Core
{
    public class ProjectMapper : IProjectMapper
    {
        private readonly IMapper mapper;

        public ProjectMapper(ILocatorService locator)
        {
            mapper = new MapperConfiguration(config =>
            {
                config.ConstructServicesUsing(locator.Locate);
                MapToDomain(config);
                MapFromDomain(config);
            }).CreateMapper();
        }

        private static void MapFromDomain(IMapperConfigurationExpression config)
        {
            config.CreateMap<Project, Domain.ViewModels.Project>(MemberList.Source)
                .ConstructUsingServiceLocator();
            config.CreateMap<Document, Domain.ViewModels.Document>(MemberList.Source)
                .ConstructUsingServiceLocator();

            config.CreateMap<Graphic, Domain.ViewModels.Item>(MemberList.Source)
                .IncludeAllDerived();
        }

        private static void MapToDomain(IMapperConfigurationExpression config)
        {
            config.CreateMap<Domain.ViewModels.Project, Domain.Models.Project>(MemberList.Destination);
            config.CreateMap<Domain.ViewModels.Document, Document>(MemberList.Destination);

            config.CreateMap<Domain.ViewModels.Item, Graphic>(MemberList.Source)
                .IncludeAllDerived();
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