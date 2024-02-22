using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.GUI.Commands;
using FestivalApp.GUI.Extensions;
using FestivalApp.GUI.Messages.ArtistMessages;
using FestivalApp.GUI.Services;
using FestivalApp.GUI.ViewModels.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FestivalApp.GUI.ViewModels
{
    public class ArtistListViewModel : ViewModelBase, IViewModel
    {
        private readonly ArtistRepository _artistRepository;

        private ObservableCollection<ArtistListModel> artists = new ObservableCollection<ArtistListModel>();

        public ICommand SelectedArtistCommand { get; }

        public ArtistListViewModel(ArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;

            SelectedArtistCommand = new RelayCommand<ArtistListModel>(ArtistSelected);

            Mediator.Instance.Register<ReloadArtistMessage>(Reload);

            Load();
        }

        public ObservableCollection<ArtistListModel> Artists
        {
            get { return artists; }
            private set
            {
                artists = value;
                OnPropertyChanged(nameof(artists));
            }
        }

        private void ArtistSelected(ArtistListModel model)
        {
            Mediator.Instance.Send(new SelectedArtistMessage(model.Id));
        }

        public void Reload(ReloadArtistMessage message) => Load();

        public void Load()
        {
            Artists.Clear();
            var artists = _artistRepository.GetAll();
            Artists.AddRange(artists);
        }
    }
}
