using FestivalApp.BL.Models;
using System;

namespace FestivalApp.BL.Interfaces
{
    public interface IAddress : IRepository<AddressDetailModel>
    {
        AddressDetailModel GetByUser(Guid userId);
    }
}