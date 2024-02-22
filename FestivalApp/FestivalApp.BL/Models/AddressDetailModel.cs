using System;

namespace FestivalApp.BL.Models
{
    public class AddressDetailModel : ModelBase
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public Guid UserId { get; set; }
    }
}
