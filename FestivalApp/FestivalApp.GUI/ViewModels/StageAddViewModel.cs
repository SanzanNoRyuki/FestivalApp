using FestivalApp.BL.Repositories;
using FestivalApp.GUI.ViewModels.Interfaces;

namespace FestivalApp.GUI.ViewModels
{
    public class StageAddViewModel : ViewModelBase, IViewModel
    {
        private readonly StageRepository _stageRepository;

        public StageAddViewModel(StageRepository stageRepository)
        {
            _stageRepository = stageRepository;
        }

        public void Load()
        {

        }
    }
}
