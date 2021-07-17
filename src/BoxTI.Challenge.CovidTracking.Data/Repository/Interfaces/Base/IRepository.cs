using BoxTI.Challenge.CovidTracking.Models.Core.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxTI.Challenge.CovidTracking.Data.Repository.Interfaces.Base
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<bool> AddInfo(IEnumerable<TEntity> model);
        Task<bool> UpdateInfo(TEntity model);
    }
}
