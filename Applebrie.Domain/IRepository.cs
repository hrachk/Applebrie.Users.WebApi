using System.Collections;
 

namespace Applebrie.Domain
{
    public interface IRepository<T> : IQueryable<T>, IEnumerable<T>, IEnumerable, IQueryable, IAsyncEnumerable<T> where T : EntityBase
    {
        /// <summary>
        /// Returns a request for an object by Id
        /// </summary>
        public IQueryable<T> GetByIdQuery(Guid id);

        /// <summary>
        /// Returns object by id
        /// </summary>
        public T? GetById(Guid id);

        /// <summary>
        /// Returns objects by their id
        /// </summary>
        public List<T> GetByIds(List<Guid> ids);

        /// <summary>
        /// Returns an object by its id (asynchronous)
        /// </summary>
        public Task<T?> GetByIdAsync(Guid id);

        /// <summary>
        ///Adds an object
        /// </summary>
        public void Add(T objectToAdd);
    }
}
