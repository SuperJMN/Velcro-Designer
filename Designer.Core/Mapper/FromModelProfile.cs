using System.Collections.Generic;
using AutoMapper;
using Designer.Domain.Models;

namespace Designer.Core.Mapper
{
    public class FromModelProfile : Profile
    {
        public FromModelProfile()
        {

            CreateMap<Project, Domain.ViewModels.Project>(MemberList.Source)
                .ConstructUsingServiceLocator();
            CreateMap<Document, Domain.ViewModels.Document>(MemberList.Source)
                .ForMember(x => x.Graphics, e => e.Ignore())
                .AfterMap((a, b, r) => b.Add(r.Mapper.Map<IEnumerable<Domain.ViewModels.Item>>(a.Graphics)))
                .ConstructUsingServiceLocator();

            CreateMap<Domain.Models.Item, Domain.ViewModels.Item>(MemberList.Source)
                .IncludeAllDerived();

            CreateMap<Domain.Models.Body, Domain.ViewModels.Body>(MemberList.Source)
                .IncludeAllDerived();

            CreateMap<Domain.Models.EllipseShape, Domain.ViewModels.EllipseShape>(MemberList.Source);
            CreateMap<Domain.Models.WheelJoint, Domain.ViewModels.WheelJoint>(MemberList.Source);
        }
    }
}