using AutoMapper;

namespace Designer.Core.Mapper
{
    public class ToModelProfile : Profile
    {
        public ToModelProfile()
        {
            CreateMap<Domain.ViewModels.Project, Domain.Models.Project>(MemberList.Destination);
            CreateMap<Domain.ViewModels.Document, Domain.Models.Document>(MemberList.Destination);
            CreateMap<Domain.ViewModels.Item, Domain.Models.Item>(MemberList.Destination)
                .IncludeAllDerived();

            CreateMap<Domain.ViewModels.Body, Domain.Models.Body>(MemberList.Destination)
                .IncludeAllDerived();

            CreateMap<Domain.ViewModels.EllipseShape, Domain.Models.EllipseShape>(MemberList.Destination);
            CreateMap<Domain.ViewModels.WheelJoint, Domain.Models.WheelJoint>(MemberList.Destination);
        }
    }
}