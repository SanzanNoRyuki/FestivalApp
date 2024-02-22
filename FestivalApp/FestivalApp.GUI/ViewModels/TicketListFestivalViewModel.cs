using FestivalApp.BL.Models;
using FestivalApp.BL.Repositories;
using FestivalApp.GUI.Commands;
using FestivalApp.GUI.Extensions;
using FestivalApp.GUI.Messages.FestivalMessages;
using FestivalApp.GUI.Messages.TicketMessages;
using FestivalApp.GUI.Services;
using FestivalApp.GUI.ViewModels.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace FestivalApp.GUI.ViewModels
{
    public class TicketListFestivalViewModel : ViewModelBase, IViewModel
    {
        private readonly TicketRepository _ticketRepository;

        private Guid FestivalId;

        private ObservableCollection<TicketListModel> tickets = new ObservableCollection<TicketListModel>();

        public ICommand BuyTicketCommand { get; set; }

        public TicketListFestivalViewModel(TicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;

            Mediator.Instance.Register<TicketReloadMessage>(ReloadTickets);
            Mediator.Instance.Register<TicketsLoadByFestivalMessage>(SetFestivalId);
            Mediator.Instance.Register<ReloadFestivalMessage>(ReloadTicketsAfterFestivalsChanged);

            BuyTicketCommand = new RelayCommand<TicketListModel>(BuyTicketAction);

            Load();
        }

        public ObservableCollection<TicketListModel> Tickets
        {
            get { return tickets; }
            private set
            {
                tickets = value;
                OnPropertyChanged();
            }
        }

        private void BuyTicketAction(TicketListModel model)
        {
            TicketDetailModel variable = new TicketDetailModel()
            {
                Id = model.Id,
                Type = model.Type,
                Length = model.Length,
                PriceAmount = model.PriceAmount,
                PriceCurrency = model.PriceCurrency,
                StartDate = model.StartDate,
                FestivalId = model.FestivalId,
                FestivalEnd = model.FestivalEnd,
                FestivalStart = model.FestivalStart,
                FestivalTitle = model.FestivalTitle
            };
            variable.UserId = Guid.Parse("4e70d683-fa6c-4384-8564-30bb2ec4af3a");
            _ticketRepository.CreateOrUpdate(variable);
            Mediator.Instance.Send(new TicketReloadMessage());
            MessageBox.Show("You bought a " + model.Type + " ticket to " + model.FestivalTitle + "!");
        }

        public void SetFestivalId(TicketsLoadByFestivalMessage message)
        {
            FestivalId = message.Id;
            Load();
        }

        public void ReloadTickets(TicketReloadMessage message) => Load();
        public void ReloadTicketsAfterFestivalsChanged(ReloadFestivalMessage message) => Load();
        public void Load()
        {
            Tickets.Clear();
            var tickets = _ticketRepository.GetByFestival(FestivalId);
            Tickets.AddRange(tickets);
        }
    }
}