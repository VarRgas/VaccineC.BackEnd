using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly VaccineCCommandContext Db;

        public RepositoryBase(VaccineCCommandContext db)
        {
            Db = db;
        }

        public void Add(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity obj)
        {
            Db.Set<TEntity>().Remove(obj);
        }

        public void UpdateRowVersionTimestamp(TEntity obj, byte[] rowVersion)
        {
            Db.Entry(obj).OriginalValues["Timestamp"] = rowVersion;
        }

        public async Task SaveChangesAsync()
        {
            await Db.SaveChangesAsync();
        }
    }
}
