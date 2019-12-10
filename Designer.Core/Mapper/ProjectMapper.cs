using AutoMapper;
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
                config.ConstructServicesUsing(locator.Locate);
                config.AddProfile<FromModelProfile>();
                config.AddProfile<ToModelProfile>();
            }).CreateMapper();
        }

        //private static void MapFromDomain(IProfileExpression config)
        //{
        //    config.CreateMap<Project, Domain.ViewModels.Project>(MemberList.Source)
        //        .ConstructUsingServiceLocator();
        //    config.CreateMap<Document, Domain.ViewModels.Document>(MemberList.Source)
        //        .ForMember(x => x.Graphics, e => e.Ignore())
        //        .AfterMap((a, b, r) => b.Add(r.Mapper.Map<IEnumerable<Item>>(a.Graphics)))
        //        .ConstructUsingServiceLocator();

        //    config.CreateMap<Domain.Models.Item, Domain.ViewModels.Item>(MemberList.Source)
        //        .IncludeAllDerived();

        //    config.CreateMap<Domain.Models.Body, Domain.ViewModels.Body>(MemberList.Source)
        //        .IncludeAllDerived();

        //    config.CreateMap<Domain.Models.EllipseShape, Domain.ViewModels.EllipseShape>(MemberList.Source);
        //    config.CreateMap<Domain.Models.WheelJoint, Domain.ViewModels.WheelJoint>(MemberList.Source);

        //}

        //private static void MapToDomain(IProfileExpression config)
        //{
        //    config.CreateMap<Domain.ViewModels.Project, Domain.Models.Project>(MemberList.Destination);
        //    config.CreateMap<Domain.ViewModels.Document, Domain.Models.Document>(MemberList.Destination);
        //    config.CreateMap<Domain.ViewModels.Item, Domain.Models.Item>(MemberList.Destination)
        //        .IncludeAllDerived();

        //    config.CreateMap<Domain.ViewModels.Body, Domain.Models.Body>(MemberList.Destination)
        //        .IncludeAllDerived();
            
        //    config.CreateMap<Domain.ViewModels.EllipseShape, Domain.Models.EllipseShape>(MemberList.Destination);
        //    config.CreateMap<Domain.ViewModels.WheelJoint, Domain.Models.WheelJoint>(MemberList.Destination);
        //}

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