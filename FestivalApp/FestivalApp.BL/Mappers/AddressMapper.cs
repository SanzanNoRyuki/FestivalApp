using FestivalApp.BL.Models;
using FestivalApp.DAL.Entities;

namespace FestivalApp.BL.Mappers
{
    public class AddressMapper
    {
        public static AddressDetailModel AddressToAddressDetailModel(Address address)
        {
            if (address == null) return null;
            return new AddressDetailModel
            {
                Id = address.Id,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                Country = address.Country,
                PostalCode = address.PostalCode,
                UserId = address.UserId
            };
        }

        public static Address AddressDetailModelToAddress(AddressDetailModel addressDetailModel)
        {
            if (addressDetailModel == null) return null;
            return new Address
            {
                Id = addressDetailModel.Id,
                AddressLine1 = addressDetailModel.AddressLine1,
                AddressLine2 = addressDetailModel.AddressLine2,
                City = addressDetailModel.City,
                State = addressDetailModel.State,
                Country = addressDetailModel.Country,
                PostalCode = addressDetailModel.PostalCode,
                UserId = addressDetailModel.UserId
            };
        }
    }
}