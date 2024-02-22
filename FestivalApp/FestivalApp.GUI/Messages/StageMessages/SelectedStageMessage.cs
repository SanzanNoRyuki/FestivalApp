using FestivalApp.BL.Models;

namespace FestivalApp.GUI.Messages.StageMessages
{
    public class SelectedStageMessage : IMessage
    {
        public StageListModel model { get; }

        public SelectedStageMessage(StageListModel Model)
        {
            model = Model;
        }
    }
}
