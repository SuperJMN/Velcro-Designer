using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using Zafiro.Core;

namespace Designer.Core
{
    public class MainViewModel : ReactiveObject
    {
        private readonly IProjectStore projectStore;
        private readonly ObservableAsPropertyHelper<Project> project;
        private readonly ObservableAsPropertyHelper<bool> isBusy;
        private readonly ISubject<Project> fileLoader = new Subject<Project>();

        public MainViewModel(IFilePicker filePicker, IViewModelFactory viewModelFactory, IProjectStore projectStore)
        {
            this.projectStore = projectStore;

            var saveExtensions = new[] { new KeyValuePair<string, IList<string>>(Constants.FileFormatName, new List<string> { Constants.FileFormatExtension }) };

            Load = ReactiveCommand.CreateFromObservable(() => LoadProject(filePicker, new[] { Constants.FileFormatExtension }));
            New = ReactiveCommand.Create(viewModelFactory.CreateProject);
            Save = ReactiveCommand.CreateFromObservable(() => SaveProject(filePicker, Project, saveExtensions));
            LoadFromFile = ReactiveCommand.CreateFromTask<ZafiroFile, Project>(e => LoadProject(e));

            var projects = Load.Merge(New).Merge(LoadFromFile);
            project = projects.ToProperty(this, model => model.Project);

            isBusy = Load.IsExecuting.Merge(Save.IsExecuting).Merge(LoadFromFile.IsExecuting).ToProperty(this, x => x.IsBusy);

            New.Execute().Subscribe();
        }

        public ReactiveCommand<ZafiroFile, Project> LoadFromFile { get; }

        public bool IsBusy => isBusy.Value;

        public ReactiveCommand<Unit, Project> Save { get; }

        public Project Project => project.Value;

        public ReactiveCommand<Unit, Project> New { get; }

        private IObservable<Project> LoadProject(IFilePicker filePicker, string[] loadExtensions)
        {
            return filePicker.Pick("Load", loadExtensions)
                .SelectMany(LoadProject);
        }

        private async Task<Project> LoadProject(ZafiroFile file)
        {
            var plugin = projectStore;
            if (plugin == null)
            {
                throw new InvalidOperationException("No plugins to load this file type!");
            }

            using (var stream = await file.OpenForRead())
            {
                return await plugin.Load(stream);
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
                await projectStore.Save(project, stream);
                await stream.FlushAsync();
                return project;
            }
        }

        public ReactiveCommand<Unit, Project> Load { get; }

        public async Task LoadFile(ZafiroFile file)
        {
            var proj = await LoadProject(file);
            fileLoader.OnNext(proj);
        }
    }
}