using System;

namespace FestivalApp.GUI.Messages.TicketMessages
{
    public class TicketsLoadByUserMessage : IMessage
    {
        public Guid Id { get; set; }

        public TicketsLoadByUserMessage(Guid id)
        {
            Id = id;
        }
    }
}
