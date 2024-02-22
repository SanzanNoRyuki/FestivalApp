using FestivalApp.BL.Repositories;
using FestivalApp.GUI.ViewModels.Interfaces;
using FestivalApp.BL.Models;
using System;
using FestivalApp.GUI.Services;
using FestivalApp.GUI.Messages.ShowMessages;
using FestivalApp.GUI.Messages;
using System.Windows;
using System.Windows.Input;
using FestivalApp.GUI.Commands;
using FestivalApp.GUI.Messages.StageMessages;
using System.Collections;
using FestivalApp.GUI.Messages.ArtistMessages;

namespace FestivalApp.GUI.ViewModels
{
    public class ShowDetailViewModel : ViewModelBase, IViewModel
    {
        private readonly ShowRepository _showRepository;
        private readonly StageRepository _stageRepository;
        private readonly ArtistRepository _artistRepository;

        private ShowDetailModel show;

        public IEnumerable ArtistList { get; set; }
        public ArtistListModel artist { get; set; }

        public Guid id { get; set; }
        public Guid StageID { get; set; }

        public ICommand AddShowCommand { get; }
        public ICommand EditShowCommand { get; }
        public ICommand DeleteShowCommand { get; }
        public ICommand SaveCommand { get; }

        public ShowDetailViewModel(ShowRepository showRepository, StageRepository stageRepository, ArtistRepository artistRepository)
        {
            _showRepository = showRepository;
            _stageRepository = stageRepository;
            _artistRepository = artistRepository;

            ArtistList = _artistRepository.GetAll();

            Mediator.Instance.Register<SelectedShowMessage>(ShowSelected);
            Mediator.Instance.Register<StageIDSendMessage>(StageIdSetValue);
            Mediator.Instance.Register<ReloadArtistMessage>(ReloadArtistList);

            AddShowCommand = new RelayCommand(AddShow);
            EditShowCommand = new RelayCommand(EditShow);
            DeleteShowCommand = new RelayCommand<Guid>(DeleteShow);
            SaveCommand = new RelayCommand<ShowDetailModel>(SaveShow);

            Load();
        }

        public ShowDetailModel Show
        {
            get
            {
                return show;
            }
            set
            {
                show = value;
                OnPropertyChanged();
            }
        }

        private void AddShow()
        {
            Show = new ShowDetailModel();
            OnPropertyChanged(nameof(Show));
            Mediator.Instance.Send(new AddedShowMessage());
        }

        private void EditShow()
        {
            Mediator.Instance.Send(new AddedShowMessage());
        }

        private void DeleteShow(Guid id)
        {
            _showRepository.Delete(id);
            Mediator.Instance.Send(new ReloadShowMessage());
            Mediator.Instance.Send(new NavBarMessage("Home"));
            MessageBox.Show("Show deleted!");
        }

        private void SaveShow(ShowDetailModel model)
        {
            var stage = _stageRepository.GetById(StageID);
            model.StageId = StageID;
            model.StageName = stage.Name;
            model.StagePhotoPath = stage.PhotoPath;
            model.StageDescription = stage.Description;
            model.ArtistId = artist.Id;
            model.ArtistGenre = artist.Genre;
            model.ArtistName = artist.Name;
            Show = _showRepository.CreateOrUpdate(model);
            OnPropertyChanged(nameof(show));
            Load();
            Mediator.Instance.Send(new ReloadShowMessage());
            Mediator.Instance.Send(new NavBarMessage("Home"));
            MessageBox.Show("Show saved!");
        }

        public void ReloadArtistList(ReloadArtistMessage message) => ArtistList = _artistRepository.GetAll();

        public void StageIdSetValue(StageIDSendMessage message)
        {
            StageID = message.model.Id;
        }

        public void ShowSelected(SelectedShowMessage message)
        {
            id = message.Id;
            Load();
        }

        public void Load()
        {
            Show = _showRepository.GetById(id) ?? new ShowDetailModel();
        }
    }
}
