using FestivalApp.BL.Mappers;
using FestivalApp.DAL.Entities;
using Xunit;

namespace FestivalApp.BL.Tests.MapperTests
{
    public class AddressTests
    {
        public AreEqualMethod AreEqual = new AreEqualMethod();

        [Theory]
        [InlineData("aaaaa", "bbbbb", "ccccc", "ddddd", "eeeee")]
        [InlineData("\0", null, "halo", "0000000000", "+íšíčíříížíší")]
        [InlineData(";;Tohle", ";;Jsou", ";;Normální", ";;Data", "+ěščřřžýýáííé-*+/")]
        [InlineData(null, null, null, null, null)]
        public void Map_Basic_Values(string addressLine, string city, string country, string postalCode, string state)
        {
            AreEqualMethod AreEqual = new AreEqualMethod();
            var addressEntity = new Address()
            {
                AddressLine1 = addressLine,
                AddressLine2 = "hello",
                City = city,
                Country = country,
                PostalCode = postalCode,
                State = state
            };

            var addressDetail = AddressMapper.AddressToAddressDetailModel(addressEntity);

            AreEqual.Equals(addressEntity.AddressLine1, addressDetail.AddressLine1, addressLine);
            AreEqual.Equals(addressEntity.City, addressDetail.City, city);
            AreEqual.Equals(addressEntity.Country, addressDetail.Country, country);
            AreEqual.Equals(addressEntity.PostalCode, addressDetail.PostalCode, postalCode);
            AreEqual.Equals(addressEntity.State, addressDetail.State, state);

            var newAddressEntity = AddressMapper.AddressDetailModelToAddress(addressDetail);

            AreEqual.Equals(addressDetail.AddressLine1, newAddressEntity.AddressLine1, addressLine);
            AreEqual.Equals(addressDetail.City, newAddressEntity.City, city);
            AreEqual.Equals(addressDetail.Country, newAddressEntity.Country, country);
            AreEqual.Equals(addressDetail.PostalCode, newAddressEntity.PostalCode, postalCode);
            AreEqual.Equals(addressDetail.State, newAddressEntity.State, state);
        }
    }
}
