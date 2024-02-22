using System;

namespace FestivalApp.GUI.Messages.FestivalMessages
{
    public class SelectedFestivalMessage : IMessage
    {
        public Guid Id { get; }

        public SelectedFestivalMessage(Guid ID)
        {
            Id = ID;
        }
    }
}
