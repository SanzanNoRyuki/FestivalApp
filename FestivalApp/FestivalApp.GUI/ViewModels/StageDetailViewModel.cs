using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.GUI.Commands;
using FestivalApp.GUI.Messages;
using FestivalApp.GUI.Messages.FestivalMessages;
using FestivalApp.GUI.Messages.ShowMessages;
using FestivalApp.GUI.Messages.StageMessages;
using FestivalApp.GUI.Services;
using FestivalApp.GUI.ViewModels.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace FestivalApp.GUI.ViewModels
{
    public class StageDetailViewModel : ViewModelBase, IViewModel
    {
        private readonly StageRepository _stageRepository;
        private readonly ShowRepository _showRepository;
        private readonly FestivalRepository _festivalRepository;

        private StageDetailModel stage;

        public BitmapImage StageImage { get; set; }

        public ShowListViewModel ShowListViewModel { get; set; }

        public Guid id { get; set; }
        public Guid FestivalID { get; set; }

        public ICommand AddShowCommand { get; }
        public ICommand EditStageCommand { get; }
        public ICommand DeleteStageCommand { get; }
        public ICommand SaveCommand { get; }

        public StageDetailViewModel(StageRepository stageRepository, ShowRepository showRepository, FestivalRepository festivalRepository)
        {
            _stageRepository = stageRepository;
            _showRepository = showRepository;
            _festivalRepository = festivalRepository;

            ShowListViewModel = new ShowListViewModel(_showRepository);

            Mediator.Instance.Register<SelectedStageMessage>(StageSelected);
            Mediator.Instance.Register<FestivalIDSendMessage>(FestivalIDSetValue);
            Mediator.Instance.Register<ReloadShowMessage>(ReloadShows);

            AddShowCommand = new RelayCommand<StageDetailModel>(AddShowPage);
            EditStageCommand = new RelayCommand(EditStage);
            DeleteStageCommand = new RelayCommand<Guid>(DeleteStage);
            SaveCommand = new RelayCommand<StageDetailModel>(SaveStage);

            Load();
        }

        public StageDetailModel Stage
        {
            get
            {
                return stage;
            }
            set
            {
                stage = value;
                OnPropertyChanged(nameof(stage));
            }
        }

        private void AddShowPage(StageDetailModel model)
        {
            Mediator.Instance.Send(new AddedShowMessage());
            Mediator.Instance.Send(new StageIDSendMessage(model));
        }

        private void EditStage()
        {
            Mediator.Instance.Send(new AddedStageMessage());
        }

        private void DeleteStage(Guid id)
        {
            _stageRepository.Delete(id);
            Mediator.Instance.Send(new ReloadStageMessage());
            Mediator.Instance.Send(new NavBarMessage("Home"));
            MessageBox.Show("Stage deleted!");
        }

        private void SaveStage(StageDetailModel model)
        {
            var festival = _festivalRepository.GetById(FestivalID);
            model.FestivalId = FestivalID;
            model.FestivalStart = festival.Start;
            model.FestivalEnd = festival.End;
            model.FestivalTitle = festival.Title;
            model.FestivalCapacity = festival.Capacity;

            stage = _stageRepository.CreateOrUpdate(model);
            OnPropertyChanged(nameof(stage));
            Load();
            Mediator.Instance.Send(new ReloadStageMessage());
            Mediator.Instance.Send(new NavBarMessage("Home"));
            MessageBox.Show("Stage saved!");
        }

        public void ReloadShows(ReloadShowMessage message)
        {
            Mediator.Instance.Send(new ShowMessage(id, "Stage"));
        }


        public void StageSelected(SelectedStageMessage message)
        {
            id = message.model.Id;
            Load();
        }

        public void FestivalIDSetValue(FestivalIDSendMessage message)
        {
            FestivalID = message.model.Id;
            stage = new StageDetailModel();
        }

        public void Load()
        {
            stage = _stageRepository.GetById(id) ?? new StageDetailModel();
            if (stage.PhotoPath != null)
            {
                StageImage = new BitmapImage(new Uri(stage.PhotoPath, UriKind.Relative));
            }
            Mediator.Instance.Send(new ShowMessage(id, "Stage"));
            Mediator.Instance.Send(new StageIDSendMessage(stage));
        }
    }
}
