using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.GUI.Commands;
using FestivalApp.GUI.Extensions;
using FestivalApp.GUI.Messages.StageMessages;
using FestivalApp.GUI.Services;
using FestivalApp.GUI.ViewModels.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FestivalApp.GUI.ViewModels
{
    public class StageListViewModel : ViewModelBase, IViewModel
    {
        private readonly StageRepository _stageRepository;

        private ObservableCollection<StageListModel> stages = new ObservableCollection<StageListModel>();

        public ICommand SelectedStageCommand { get; }

        public Guid id { get; set; }

        public StageListViewModel(StageRepository stageRepository)
        {
            _stageRepository = stageRepository;

            SelectedStageCommand = new RelayCommand<StageListModel>(StageSelected);

            Mediator.Instance.Register<ReloadStageMessage>(Reload);
            Mediator.Instance.Register<StageLoadByFestivalMessage>(LoadByFestivalId);
        }

        public ObservableCollection<StageListModel> Stages
        {
            get { return stages; }
            private set
            {
                stages = value;
                OnPropertyChanged(nameof(stages));
            }
        }

        private void StageSelected(StageListModel model)
        {
            Mediator.Instance.Send(new SelectedStageMessage(model));
        }

        public void Reload(ReloadStageMessage message) => Load();

        public void LoadByFestivalId(StageLoadByFestivalMessage message)
        {
            id = message.Id;
            Load();
        }

        public void Load()
        {
            Stages.Clear();
            var stages = _stageRepository.GetByFestival(id);
            Stages.AddRange(stages);
        }
    }
}
