using FestivalApp.BL.Mappers;
using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.GUI.Commands;
using FestivalApp.GUI.Messages;
using FestivalApp.GUI.Messages.FestivalMessages;
using FestivalApp.GUI.Messages.StageMessages;
using FestivalApp.GUI.Messages.TicketMessages;
using FestivalApp.GUI.Messages.UserMessages;
using FestivalApp.GUI.Services;
using FestivalApp.GUI.ViewModels.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;

namespace FestivalApp.GUI.ViewModels
{
    public class FestivalDetailViewModel : ViewModelBase, IViewModel
    {
        private readonly FestivalRepository _festivalRepository;
        private readonly UserRepository _userRepository;
        private readonly StageRepository _stageRepository;

        private FestivalDetailModel festival;
        private UserDetailModel user;
        private StageDetailModel stage;

        public Guid id { get; set; }

        public StageListViewModel StageListViewModel { get; set; }

        public ICommand BuyTicket { get; }
        public ICommand AddTicketCommand { get; }
        public ICommand AddFestivalCommand { get; }
        public ICommand EditFestivalCommand { get; }
        public ICommand DeleteFestivalCommand { get; }

        public ICommand AddStageCommand { get; }
        public ICommand SaveFestivalCommand { get; }

        public FestivalDetailViewModel(FestivalRepository festivalRepository, UserRepository userRepository, StageRepository stageRepository)
        {
            _festivalRepository = festivalRepository;
            _userRepository = userRepository;
            _stageRepository = stageRepository;

            user = _userRepository.GetById(Guid.Parse("4e70d683-fa6c-4384-8564-30bb2ec4af3a"));
            StageListViewModel = new StageListViewModel(_stageRepository);

            Mediator.Instance.Register<SelectedFestivalMessage>(FestivalSelected);
            Mediator.Instance.Register<SaveUserMessage>(ReloadOnUserChange);

            BuyTicket = new RelayCommand(BuyTicketAction);
            AddTicketCommand = new RelayCommand<FestivalDetailModel>(AddTicketAction);
            AddFestivalCommand = new RelayCommand(AddFestival);
            EditFestivalCommand = new RelayCommand(EditFestival);
            DeleteFestivalCommand = new RelayCommand<Guid>(DeleteFestival);

            AddStageCommand = new RelayCommand<FestivalDetailModel>(AddStageAction);
            SaveFestivalCommand = new RelayCommand<FestivalDetailModel>(SaveFestivalAction);
        }

        public FestivalDetailModel Festival
        {
            get
            {
                return festival;
            }
            set
            {
                festival = value;
                OnPropertyChanged(nameof(festival));
            }
        }

        public StageDetailModel Stage
        {
            get
            {
                return stage;
            }
            set
            {
                stage = value;
                OnPropertyChanged(nameof(stage));
            }
        }

        public void FestivalSelected(SelectedFestivalMessage message)
        {
            id = message.Id;
            Load();
        }

        public void ReloadOnUserChange(SaveUserMessage message)
        {
            Load();
        }

        private void AddTicketAction(FestivalDetailModel model)
        {
            Mediator.Instance.Send(new TicketAddPageMessage());
        }

        private void BuyTicketAction()
        {
            Mediator.Instance.Send(new TicketBuyMessage());
        }

        private void AddFestival()
        {
            festival = new FestivalDetailModel();
            OnPropertyChanged(nameof(festival));
            Mediator.Instance.Send(new AddedFestivalMessage());
        }

        private void EditFestival()
        {
            Mediator.Instance.Send(new AddedFestivalMessage());
        }

        private void DeleteFestival(Guid id)
        {
            _festivalRepository.Delete(id);
            Mediator.Instance.Send(new ReloadFestivalMessage());
            Mediator.Instance.Send(new NavBarMessage("Home"));
            MessageBox.Show("Festival deleted!");
        }

        private void SaveFestivalAction(FestivalDetailModel model)
        {
            if (model.Id == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                model.UserId = user.Id;
                model.UserEmail = user.Email;
                model.UserAddressId = user.AddressId;
                model.UserName = user.Name;
                model.UserPhoneNumber = user.PhoneNumber;
            }
            if (Stage != null)
            {
                model.Stages.Add(StageMapper.StageDetailModelToStageListModel(Stage));
            }
            festival = _festivalRepository.CreateOrUpdate(model);
            OnPropertyChanged(nameof(festival));
            Load();
            Mediator.Instance.Send(new ReloadFestivalMessage());
            Mediator.Instance.Send(new NavBarMessage("Home"));
            MessageBox.Show("Festival saved!");
        }

        private void AddStageAction(FestivalDetailModel model)
        {
            if (model.Id != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                Mediator.Instance.Send(new FestivalIDSendMessage(model));
                Mediator.Instance.Send(new AddedStageMessage());
            }
        }

        public void Load()
        {
            festival = _festivalRepository.GetById(id) ?? new FestivalDetailModel();
            Mediator.Instance.Send(new StageLoadByFestivalMessage(id));
            Mediator.Instance.Send(new FestivalIDSendMessage(festival));
            Mediator.Instance.Send(new TicketsLoadByFestivalMessage(id));
        }
    }
}
