using System;

namespace FestivalApp.GUI.Messages.ShowMessages
{
    public class SelectedShowMessage : IMessage
    {
        public Guid Id { get; }

        public SelectedShowMessage(Guid ID)
        {
            Id = ID;
        }
    }
}
