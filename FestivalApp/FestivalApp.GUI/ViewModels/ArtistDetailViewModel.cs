using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.GUI.Commands;
using FestivalApp.GUI.Messages;
using FestivalApp.GUI.Messages.ArtistMessages;
using FestivalApp.GUI.Messages.ShowMessages;
using FestivalApp.GUI.Services;
using FestivalApp.GUI.ViewModels.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FestivalApp.GUI.ViewModels
{
    public class ArtistDetailViewModel : ViewModelBase, IViewModel
    {
        private readonly ArtistRepository _artistRepository;
        private readonly ShowRepository _showRepository;

        private ArtistDetailModel artist;

        public ImageSource ArtistImage { get; set; }

        public ShowListViewModel ShowListViewModel { get; set; }

        public Guid id { get; set; }

        public ICommand AddArtistCommand { get; }
        public ICommand EditArtistCommand { get; }
        public ICommand DeleteArtistCommand { get; }
        public ICommand SaveCommand { get; }

        public ArtistDetailViewModel(ArtistRepository artistRepository, ShowRepository showRepository)
        {
            _artistRepository = artistRepository;
            _showRepository = showRepository;

            ShowListViewModel = new ShowListViewModel(_showRepository);

            Mediator.Instance.Register<SelectedArtistMessage>(ArtistSelected);
            Mediator.Instance.Register<ReloadShowMessage>(ReloadShows);

            AddArtistCommand = new RelayCommand(AddArtist);
            EditArtistCommand = new RelayCommand(EditArtist);
            DeleteArtistCommand = new RelayCommand<Guid>(DeleteArtist);
            SaveCommand = new RelayCommand<ArtistDetailModel>(SaveArtist);

            Load();
        }

        public ArtistDetailModel Artist
        {
            get
            {
                return artist;
            }
            set
            {
                artist = value;
                OnPropertyChanged(nameof(artist));
            }
        }

        private void ArtistSelected(SelectedArtistMessage message)
        {
            id = message.Id;
            Load();
        }

        private void AddArtist()
        {
            artist = new ArtistDetailModel();
            OnPropertyChanged(nameof(artist));
            Mediator.Instance.Send(new AddedArtistMessage());
        }

        private void EditArtist()
        {
            Mediator.Instance.Send(new AddedArtistMessage());
        }

        private void DeleteArtist(Guid id)
        {
            _artistRepository.Delete(id);
            Mediator.Instance.Send(new ReloadArtistMessage());
            Mediator.Instance.Send(new NavBarMessage("Artists"));
            MessageBox.Show("Artist deleted!");
        }

        private void SaveArtist(ArtistDetailModel model)
        {
            artist = _artistRepository.CreateOrUpdate(model);
            OnPropertyChanged(nameof(artist));
            Load();
            Mediator.Instance.Send(new ReloadArtistMessage());
            Mediator.Instance.Send(new NavBarMessage("Artists"));
            MessageBox.Show("Artist saved!");
        }

        public void ReloadShows(ReloadShowMessage message)
        {
            Mediator.Instance.Send(new ShowMessage(id, "Artist"));
        }

        public void Load()
        {
            artist = _artistRepository.GetById(id) ?? new ArtistDetailModel();
            if (artist.PhotoPath != null)
            {
                ArtistImage = new BitmapImage(new Uri(artist.PhotoPath, UriKind.Relative));
            }
            Mediator.Instance.Send(new ShowMessage(id, "Artist"));
        }
    }
}
