using FestivalApp.BL.Repositories;
using FestivalApp.GUI.ViewModels.Interfaces;

namespace FestivalApp.GUI.ViewModels
{
    public class ShowAddViewModel : ViewModelBase, IViewModel
    {
        private readonly ShowRepository _showRepository;

        public ShowAddViewModel(ShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        public void Load()
        {

        }
    }
}
