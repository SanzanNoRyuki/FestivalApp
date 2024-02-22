using System;

namespace FestivalApp.GUI.Messages
{
    public class AddStageToFestivalMessage : IMessage
    {
        public Guid Id { get; }

        public AddStageToFestivalMessage(Guid ID)
        {
            Id = ID;
        }
    }
}
