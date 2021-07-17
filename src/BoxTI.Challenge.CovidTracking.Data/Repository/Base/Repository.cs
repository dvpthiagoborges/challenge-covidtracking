using BoxTI.Challenge.CovidTracking.Data.Context;
using BoxTI.Challenge.CovidTracking.Data.Repository.Interfaces.Base;
using BoxTI.Challenge.CovidTracking.Models.Core.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoxTI.Challenge.CovidTracking.Data.Repository.Base
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        public CovidTrackingContext _db { get; protected set; }
        public virtual DbSet<TEntity> _dbSet { get; protected set; }

        protected Repository(CovidTrackingContext context)
        {
            _db = context;
            _dbSet = _db.Set<TEntity>();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        public virtual async Task<bool> AddInfo(IEnumerable<TEntity> model)
        {
            _dbSet.AddRange(model);
            await _db.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> UpdateInfo(TEntity model)
        {
            _dbSet.Update(model);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
