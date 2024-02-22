using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.BL.Tests;
using FestivalApp.DAL;
using FestivalApp.DAL.Factories;
using FestivalApp.GUI.Commands;
using FestivalApp.GUI.Messages;
using FestivalApp.GUI.Messages.TicketMessages;
using FestivalApp.GUI.Messages.UserMessages;
using FestivalApp.GUI.Services;
using FestivalApp.GUI.ViewModels.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;

namespace FestivalApp.GUI.ViewModels
{
    public class UserDetailViewModel : ViewModelBase, IViewModel
    {
        private readonly UserRepository _userRepository;
        private readonly TicketRepository _ticketRepository;
        private readonly FestivalRepository _festivalRepository;

        private UserDetailModel user;

        public Guid id = Guid.Parse("4e70d683-fa6c-4384-8564-30bb2ec4af3a");

        public ICommand SaveCommand { get; }

        public TicketListUserViewModel TicketListUserViewModel { get; set; }
        public FestivalListViewModel FestivalListViewModel { get; set; }

        public UserDetailViewModel(UserRepository userRepository, TicketRepository ticketRepository, FestivalRepository festivalRepository)
        {
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
            _festivalRepository = festivalRepository;

            TicketListUserViewModel = new TicketListUserViewModel(_ticketRepository);
            FestivalListViewModel = new FestivalListViewModel(_festivalRepository);

            SaveCommand = new RelayCommand<UserDetailModel>(SaveUserData);

            Load();
        }

        public UserDetailModel User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged(nameof(user));
            }
        }

        private void SaveUserData(UserDetailModel model)
        {
            user = _userRepository.CreateOrUpdate(model);
            OnPropertyChanged(nameof(user));
            MessageBox.Show("User saved!");
            Load();
            Mediator.Instance.Send(new SaveUserMessage());
        }

        public void Load()
        {
            user = _userRepository.GetById(id) ?? new UserDetailModel();
            Mediator.Instance.Send(new TicketsLoadByUserMessage(id));
        }
    }
}
