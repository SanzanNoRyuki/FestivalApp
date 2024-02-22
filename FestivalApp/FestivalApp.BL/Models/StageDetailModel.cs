using System;
using Nemesis.Essentials.Design;
using System.Collections.Generic;

namespace FestivalApp.BL.Models
{
    public class StageDetailModel : ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }

        public Guid FestivalId { get; set; }
        public string FestivalTitle { get; set; }
        public DateTime FestivalStart { get; set; }
        public DateTime FestivalEnd { get; set; }
        public uint FestivalCapacity { get; set; }
        public ICollection<ShowListModel> Shows { get; set; } = new ValueCollection<ShowListModel>();
    }
}
