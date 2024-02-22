using System;

namespace FestivalApp.GUI.Messages.TicketMessages
{
    public class TicketsLoadByFestivalMessage : IMessage
    {
        public Guid Id { get; set; }

        public TicketsLoadByFestivalMessage(Guid id)
        {
            Id = id;
        }

        public TicketsLoadByFestivalMessage()
        {
        }
    }
}
