using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Designer.Domain.Models;
using Designer.Domain.ViewModels;
using DynamicData;
using Grace.DependencyInjection;
using Document = Designer.Domain.Models.Document;
using Project = Designer.Domain.Models.Project;

namespace Designer.Core
{
    public class ProjectMapper : IProjectMapper
    {
        private readonly IMapper mapper;

        public ProjectMapper(ILocatorService locator)
        {
            mapper = new MapperConfiguration(config =>
            {
                //config.CreateMap(typeof(IEnumerable<>), typeof(SourceList<>)).ConvertUsing(typeof(SourceListConverter<,>));
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
                .ForMember(x => x.Graphics, e => e.Ignore())
                .AfterMap((a, b, r) => b.Add(r.Mapper.Map<IEnumerable<Item>>(a.Graphics)))
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