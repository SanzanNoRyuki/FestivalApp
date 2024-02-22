using System;
using FestivalApp.BL.Models.Enums;

namespace FestivalApp.BL.Models
{
    public class TicketDetailModel : ModelBase
    {
        public TicketType Type { get; set; }
        public TicketLength Length { get; set; }
        public decimal PriceAmount { get; set; }
        public Currency PriceCurrency { get; set; }
        public DateTime StartDate { get; set; }

        public Guid? UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }

        public Guid FestivalId { get; set; }
        public string FestivalTitle { get; set; }
        public DateTime FestivalStart { get; set; }
        public DateTime FestivalEnd { get; set; }
    }
}
