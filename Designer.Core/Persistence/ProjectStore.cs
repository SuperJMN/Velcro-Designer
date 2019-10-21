using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using ExtendedXmlSerializer.Configuration;
using ExtendedXmlSerializer.ExtensionModel.Content;
using ExtendedXmlSerializer.ExtensionModel.Xml;
using Grace.DependencyInjection;
using Splat;

namespace Designer.Core.Persistence
{
    public class ProjectStore : IProjectStore
    {
        private readonly IExtendedXmlSerializer serializer;
        private IMapper mapper;

        public ProjectStore(ILocatorService container)
        {
            serializer = new ConfigurationContainer()
                .UseAutoFormatting()
                .UseOptimizedNamespaces()
                .EnableParameterizedContent()
                .Register(ColorConverter.Default)
                .Create();

            mapper = new MapperConfiguration(config =>
            {
                config.ConstructServicesUsing(container.Locate);


                config.CreateMap<Project, Model.Project>(MemberList.Destination)
                    .ConstructUsingServiceLocator();

                config.CreateMap<Document, Model.Document>(MemberList.Destination)
                    .ConstructUsingServiceLocator();

                config.CreateMap<Node, Model.Node>(MemberList.Destination)
                    .IncludeAllDerived();

                config.CreateMap<CircleShape, Model.CircleShape>(MemberList.Destination);


                config.CreateMap<Model.Project, Project>(MemberList.Source)
                    .ConstructUsingServiceLocator();

                config.CreateMap<Model.Document, Document>(MemberList.Source)
                    .ConstructUsingServiceLocator();

                config.CreateMap<Model.Node, Node>(MemberList.Source)
                    .IncludeAllDerived();

                config.CreateMap<Model.CircleShape, CircleShape>(MemberList.Source);


            }).CreateMapper();

            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public Task<Project> Load(Stream stream)
        {
            var model = serializer.Deserialize<Model.Project>(stream);
            var viewModel = mapper.Map<Project>(model);
            return Task.FromResult(viewModel);
        }

        public Task Save(Project project, Stream stream)
        {
            var model = mapper.Map<Model.Project>(project);
            serializer.Serialize(stream, model);
            return Task.CompletedTask;
        }
    }
}