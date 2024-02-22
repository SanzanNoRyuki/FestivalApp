using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.GUI.Commands;
using FestivalApp.GUI.Extensions;
using FestivalApp.GUI.Messages.FestivalMessages;
using FestivalApp.GUI.Messages.UserMessages;
using FestivalApp.GUI.Services;
using FestivalApp.GUI.ViewModels.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FestivalApp.GUI.ViewModels
{
    public class FestivalListViewModel : ViewModelBase, IViewModel
    {
        private readonly FestivalRepository _festivalRepository;

        private ObservableCollection<FestivalListModel> festivals = new ObservableCollection<FestivalListModel>();

        public ICommand SelectedFestivalCommand { get; }

        public FestivalListViewModel(FestivalRepository festivalRepository)
        {
            _festivalRepository = festivalRepository;

            SelectedFestivalCommand = new RelayCommand<FestivalListModel>(FestivalSelected);

            Mediator.Instance.Register<SaveUserMessage>(ReloadOnUserChanged);
            Mediator.Instance.Register<ReloadFestivalMessage>(Reload);

            Load();
        }

        public ObservableCollection<FestivalListModel> Festivals
        {
            get { return festivals; }
            private set
            {
                festivals = value;
                OnPropertyChanged(nameof(festivals));
            }
        }

        private void FestivalSelected(FestivalListModel model)
        {
            Mediator.Instance.Send(new SelectedFestivalMessage(model.Id));
        }

        public void ReloadOnUserChanged(SaveUserMessage message) => Load();
        public void Reload(ReloadFestivalMessage message) => Load();

        public void Load()
        {
            Festivals.Clear();
            var festivals = _festivalRepository.GetAll();
            Festivals.AddRange(festivals);
        }
    }
}
