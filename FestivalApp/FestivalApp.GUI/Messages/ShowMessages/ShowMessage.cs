using System;

namespace FestivalApp.GUI.Messages.ShowMessages
{
    public class ShowMessage : IMessage
    {
        public Guid Id { get; }
        public string GetBy { get; }

        public ShowMessage(Guid _id, string _getBy)
        {
            Id = _id;
            GetBy = _getBy;
        }
    }
}
