using FestivalApp.BL.Models;

namespace FestivalApp.GUI.Messages.FestivalMessages
{
    public class FestivalIDSendMessage : IMessage
    {
        public FestivalDetailModel model { get; }

        public FestivalIDSendMessage(FestivalDetailModel Model)
        {
            model = Model;
        }
    }
}
