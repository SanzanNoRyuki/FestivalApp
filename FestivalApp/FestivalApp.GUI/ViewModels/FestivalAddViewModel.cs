using FestivalApp.BL.Repositories;
using FestivalApp.GUI.ViewModels.Interfaces;

namespace FestivalApp.GUI.ViewModels
{
    public class FestivalAddViewModel : ViewModelBase, IViewModel
    {
        private readonly FestivalRepository _festivalRepository;

        public FestivalAddViewModel(FestivalRepository festivalRepository)
        {
            _festivalRepository = festivalRepository;
        }

        public void Load()
        {

        }
    }
}
