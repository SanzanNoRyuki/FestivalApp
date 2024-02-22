using System;

namespace FestivalApp.GUI.Messages.ArtistMessages
{
    public class SelectedArtistMessage : IMessage
    {
        public Guid Id { get; }

        public SelectedArtistMessage(Guid ID)
        {
            Id = ID;
        }
    }
}
