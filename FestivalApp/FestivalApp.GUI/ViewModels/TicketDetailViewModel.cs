using FestivalApp.BL.Models;
using FestivalApp.BL.Models.Enums;
using FestivalApp.BL.Repositories;
using FestivalApp.GUI.Commands;
using FestivalApp.GUI.Messages;
using FestivalApp.GUI.Messages.FestivalMessages;
using FestivalApp.GUI.Messages.TicketMessages;
using FestivalApp.GUI.Services;
using FestivalApp.GUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace FestivalApp.GUI.ViewModels
{
    public class TicketDetailViewModel : ViewModelBase, IViewModel
    {
        private readonly TicketRepository _ticketRepository;

        private TicketDetailModel ticket;

        public FestivalDetailModel Festival;

        public ICommand AddTicket { get; }

        public TicketDetailViewModel(TicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;

            AddTicket = new RelayCommand(AddTicketAction);

            Mediator.Instance.Register<FestivalIDSendMessage>(LoadFestival);
        }

        public TicketDetailModel Ticket
        {
            get
            {
                return ticket;
            }
            set
            {
                ticket = value;
                OnPropertyChanged(nameof(ticket));
            }
        }

        public int SelectedPrice { get; set; }

        public TicketType SelectedType { get; set; }

        public IEnumerable<TicketType> TypeList
        {
            get
            {
                return Enum.GetValues(typeof(TicketType)).Cast<TicketType>();
            }
        }

        public TicketLength SelectedLength { get; set; }

        public IEnumerable<TicketLength> LengthList
        {
            get
            {
                return Enum.GetValues(typeof(TicketLength)).Cast<TicketLength>();
            }
        }
        public Currency SelectedCurrency { get; set; }

        public IEnumerable<Currency> CurrencyList
        {
            get
            {
                return Enum.GetValues(typeof(Currency)).Cast<Currency>();
            }
        }


        private void AddTicketAction()
        {
            var model = new TicketDetailModel()
            {
                Type = SelectedType,
                Length = SelectedLength,
                PriceCurrency = SelectedCurrency,
                PriceAmount = SelectedPrice,
                StartDate = Festival.Start,
                FestivalId = Festival.Id,
                FestivalStart = Festival.Start,
                FestivalEnd = Festival.End,
                FestivalTitle = Festival.Title
            };
            Ticket = _ticketRepository.CreateOrUpdate(model);
            Mediator.Instance.Send(new NavBarMessage("Home"));
            Mediator.Instance.Send(new TicketReloadMessage());
            MessageBox.Show("Ticket added!");
        }

        public void LoadFestival(FestivalIDSendMessage message)
        {
            Festival = message.model;
        }

        public void Load()
        {

        }
    }
}
