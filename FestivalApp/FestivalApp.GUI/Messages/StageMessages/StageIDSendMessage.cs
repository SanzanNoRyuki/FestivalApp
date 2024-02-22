using FestivalApp.BL.Models;

namespace FestivalApp.GUI.Messages.StageMessages
{
    public class StageIDSendMessage : IMessage
    {
        public StageDetailModel model { get; }

        public StageIDSendMessage(StageDetailModel Model)
        {
            model = Model;
        }
    }
}
