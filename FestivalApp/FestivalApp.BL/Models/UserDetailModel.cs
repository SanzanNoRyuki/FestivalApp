using System;
using Nemesis.Essentials.Design;
using System.Collections.Generic;

namespace FestivalApp.BL.Models
{
    public class UserDetailModel : ModelBase
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public Guid AddressId { get; set; }

        public ICollection<TicketListModel> Tickets { get; set; } = new ValueCollection<TicketListModel>();
        public ICollection<FestivalListModel> Festivals { get; set; } = new ValueCollection<FestivalListModel>();
    }
}
