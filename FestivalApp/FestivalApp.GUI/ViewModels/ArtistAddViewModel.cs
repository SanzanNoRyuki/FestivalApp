using FestivalApp.BL.Repositories;
using FestivalApp.GUI.ViewModels.Interfaces;

namespace FestivalApp.GUI.ViewModels
{
    public class ArtistAddViewModel : ViewModelBase, IViewModel
    {
        private readonly ArtistRepository _artistRepository;

        public ArtistAddViewModel(ArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public void Load()
        {

        }
    }
}
