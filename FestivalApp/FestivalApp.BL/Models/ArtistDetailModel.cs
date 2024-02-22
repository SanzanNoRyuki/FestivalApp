using System.Collections.Generic;

namespace FestivalApp.BL.Models
{
    public class ArtistDetailModel : ModelBase
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Genre { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string PhotoPath { get; set; }

        public ICollection<ShowListModel> Shows { get; set; } = new List<ShowListModel>();

    }
}
