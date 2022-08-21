namespace VaccineC.Command.Domain.Abstractions.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        public void Add(TEntity obj);
        public TEntity GetById(Guid id);
        public IEnumerable<TEntity> GetAll();
        public void Remove(TEntity obj);
        public Task SaveChangesAsync();
    }
}
