using System;

namespace FestivalApp.GUI.Messages.StageMessages
{
    public class StageLoadByFestivalMessage : IMessage
    {
        public Guid Id { get; }

        public StageLoadByFestivalMessage(Guid ID)
        {
            Id = ID;
        }
    }
}
