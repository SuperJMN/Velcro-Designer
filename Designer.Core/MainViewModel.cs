using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Designer.Domain.ViewModels;
using ReactiveUI;
using Zafiro.Core.Files;
using Document = Designer.Domain.Models.Document;

namespace Designer.Core
{
    public class MainViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<ReactiveCommand<Unit, Unit>> align;

        private readonly KeyValuePair<string, IList<string>>[] exportExtensions =
        {
            new KeyValuePair<string, IList<string>>(Constants.FileFormatName,
                new List<string> {".txt"})
        };

        private readonly ObservableAsPropertyHelper<bool> isBusy;
        private readonly IProjectMapper mapper;
        private readonly ObservableAsPropertyHelper<Project> project;
        private readonly IProjectStore projectStore;

        private readonly KeyValuePair<string, IList<string>>[] saveExtensions =
        {
            new KeyValuePair<string, IList<string>>(Constants.FileFormatName,
                new List<string> {Constants.FileFormatExtension})
        };

        private bool isImportVisible;

        public MainViewModel(IFilePicker filePicker, IProjectMapper mapper,
            IProjectStore projectStore, IExporter exporter, IDesignContext designContext)
        {
            DesignContext = designContext;
            this.mapper = mapper;
            this.projectStore = projectStore;
            var fileOperator = new FileOperator(filePicker);

            Load = ReactiveCommand.CreateFromObservable(() =>
                LoadProject(filePicker, new[] {Constants.FileFormatExtension}));

            New = ReactiveCommand.Create(CreateNewDocument);

            Save = ReactiveCommand.CreateFromObservable(
                () =>
                {
                    return fileOperator.SaveFile(async stream =>
                        {
                            await projectStore.Save(mapper.Map(Project), stream);
                            await stream.FlushAsync();
                        },
                        saveExtensions, "Choose a file to save");
                });

            LoadFromFile = ReactiveCommand.CreateFromObservable(() =>
                fileOperator.OpenFile(projectStore.Load, new[] {Constants.FileFormatExtension}));

            Export = ReactiveCommand.CreateFromObservable(() =>
                fileOperator.SaveFile(stream => exporter.Export(mapper.Map(Project), stream), exportExtensions,
                    "Select a file"));

            var projects = Load
                .Merge(New)
                .Merge(LoadFromFile)
                .Do(_ => IsImportVisible = false);

            project = projects
                .Select(mapper.Map)
                .Do(x => x.SelectedDocument = x.Documents.FirstOrDefault())
                .ToProperty(this, model => model.Project);

            isBusy = Load.IsExecuting
                .Merge(Save.IsExecuting)
                .Merge(Export.IsExecuting)
                .Merge(LoadFromFile.IsExecuting)
                .ToProperty(this, x => x.IsBusy);

            New.Execute().Subscribe();

            ShowImport = ReactiveCommand.Create(() => IsImportVisible = true);
            HideImport = ReactiveCommand.Create(() => IsImportVisible = false);
            align = this.WhenAnyObservable(x => x.Project.SelectedDocument.AlignChanged).ToProperty(this, x => x.Align);
        }

        public IDesignContext DesignContext { get; }

        public ReactiveCommand<Unit, Unit> Align => align.Value;

        public ReactiveCommand<Unit, bool> ShowImport { get; }

        public ReactiveCommand<Unit, bool> HideImport { get; }

        public ReactiveCommand<Unit, Domain.Models.Project> LoadFromFile { get; }

        public bool IsImportVisible
        {
            get => isImportVisible;
            set => this.RaiseAndSetIfChanged(ref isImportVisible, value);
        }

        public bool IsBusy => isBusy.Value;

        public ReactiveCommand<Unit, Unit> Save { get; }

        public Project Project => project.Value;

        public ReactiveCommand<Unit, Domain.Models.Project> New { get; }

        public ReactiveCommand<Unit, Domain.Models.Project> Load { get; }

        public ReactiveCommand<Unit, Unit> Export { get; }

        private static Domain.Models.Project CreateNewDocument()
        {
            return new Domain.Models.Project
            {
                Documents = new List<Document>
                {
                    new Document
                    {
                        Name = "New document"
                    }
                }
            };
        }

        private IObservable<Domain.Models.Project> LoadProject(IFilePicker filePicker, string[] loadExtensions)
        {
            return filePicker.Pick("Load", loadExtensions)
                .Where(file => file != null)
                .SelectMany(file => LoadProject(file, projectStore));
        }

        private async Task<Domain.Models.Project> LoadProject(ZafiroFile file, IProjectStore loader)
        {
            if (loader == null)
            {
                throw new InvalidOperationException("No plugins to load this file type!");
            }

            using (var stream = await file.OpenForRead())
            {
                return await loader.Load(stream);
            }
        }

        private IObservable<Project> SaveProject(IFilePicker filePicker, Project project,
            KeyValuePair<string, IList<string>>[] loadExtensions)
        {
            return filePicker.PickSave("Save", loadExtensions)
                .SelectMany(file => SaveProject(project, file));
        }

        private async Task<Project> SaveProject(Project project, ZafiroFile file)
        {
            using (var stream = await file.OpenForWrite())
            {
                var model = mapper.Map(project);
                await projectStore.Save(model, stream);
                await stream.FlushAsync();
                return project;
            }
        }
    }
}