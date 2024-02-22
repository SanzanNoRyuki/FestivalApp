using Nemesis.Essentials.Design;
using System;
using System.Collections.Generic;

namespace FestivalApp.BL.Models
{
    public class FestivalDetailModel : ModelBase
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public uint Capacity { get; set; }

        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public Guid UserAddressId { get; set; }
        public string UserPhoneNumber { get; set; }

        public ICollection<StageListModel> Stages { get; set; } = new ValueCollection<StageListModel>();
        public ICollection<TicketListModel> Tickets { get; set; } = new ValueCollection<TicketListModel>();
    }
}
