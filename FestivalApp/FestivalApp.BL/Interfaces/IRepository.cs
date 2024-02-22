using FestivalApp.BL.Models;
using System;

namespace FestivalApp.BL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : ModelBase
    {
        TEntity GetById(Guid id);
        TEntity CreateOrUpdate(TEntity entity);
        void Delete(Guid id);
    }
}