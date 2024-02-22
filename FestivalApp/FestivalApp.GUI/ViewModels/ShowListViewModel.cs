using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.GUI.Commands;
using FestivalApp.GUI.Extensions;
using FestivalApp.GUI.Messages.ShowMessages;
using FestivalApp.GUI.Services;
using FestivalApp.GUI.ViewModels.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace FestivalApp.GUI.ViewModels
{
    public class ShowListViewModel : ViewModelBase, IViewModel
    {
        private readonly ShowRepository _showRepository;

        public Guid Id { get; set; }

        private ObservableCollection<ShowListModel> shows = new ObservableCollection<ShowListModel>();

        public ICommand SelectedShowCommand { get; }

        public ShowListViewModel(ShowRepository showRepository)
        {
            _showRepository = showRepository;

            SelectedShowCommand = new RelayCommand<ShowListModel>(ShowSelected);

            Mediator.Instance.Register<ShowMessage>(LoadBy);

            Load();
        }

        public ObservableCollection<ShowListModel> Shows
        {
            get { return shows; }
            private set
            {
                shows = value;
                OnPropertyChanged();
            }
        }

        private void ShowSelected(ShowListModel model)
        {
            Mediator.Instance.Send(new SelectedShowMessage(model.Id));
        }

        public void LoadBy(ShowMessage message)
        {
            if (message.GetBy.ToString() == "Stage")
            {
                Shows.Clear();
                var shows = _showRepository.GetByStage(message.Id);
                Shows.AddRange(shows);
            }
            else if (message.GetBy.ToString() == "Artist")
            {
                Shows.Clear();
                var shows = _showRepository.GetByArtist(message.Id);
                Shows.AddRange(shows);
            }
        }

        public void Load()
        {
        }
    }
}
