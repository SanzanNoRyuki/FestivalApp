using System;

namespace FestivalApp.BL.Models
{
    public class StageListModel : ModelBase
    {
        public string Name { get; set; }
        public Guid FestivalId { get; set; }
        public string FestivalTitle { get; set; }
        public DateTime FestivalStart { get; set; }
        public DateTime FestivalEnd { get; set; }
    }
}
