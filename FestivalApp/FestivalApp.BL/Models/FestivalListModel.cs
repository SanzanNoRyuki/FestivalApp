using System;

namespace FestivalApp.BL.Models
{
    public class FestivalListModel : ModelBase
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
    }
}
