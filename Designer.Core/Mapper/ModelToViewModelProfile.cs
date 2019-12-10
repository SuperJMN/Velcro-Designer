using System.Collections.Generic;
using AutoMapper;
using Designer.Domain.Models;
using DynamicData;

namespace Designer.Core.Mapper
{
    public class ModelToViewModelProfile : Profile
    {
        public ModelToViewModelProfile()
        {
            CreateMap<Project, Domain.ViewModels.Project>(MemberList.Source)
                .ConstructUsingServiceLocator();
            CreateMap<Document, Domain.ViewModels.Document>(MemberList.Source)
                .ForMember(x => x.ItemsCollection, e => e.Ignore())
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