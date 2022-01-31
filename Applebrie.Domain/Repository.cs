using System.Collections;
using System.Linq.Expressions;

namespace Applebrie.Domain
{

    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        #region Fields

        private readonly AppDbContext context;
        protected IQueryable<T> SourceQuery;
        public Guid Id { get; set; }


        #endregion Fields

        #region Constructor

        public Repository(AppDbContext context)
        {
            SourceQuery = context.Set<T>();

            this.context = context;
        }

        #endregion Constructor

        #region Methods

        #region Queries

        /// <summary>
        /// Returns a request for an object by Id
        /// </summary>
        public IQueryable<T> GetByIdQuery(Guid id)
        {
            return context.Set<T>().Where(o => o.Id.Equals(id));
        }


        /// <summary>
        /// Returns objects by their id
        /// </summary>
        public List<T> GetByIds(List<Guid> ids)
        {
            return context.Set<T>().Where(o => ids.Contains(o.Id)).ToList();
        }


        /// <summary>
        /// Returns an object by its id
        /// </summary>
        public T? GetById(Guid id)
        {
            return context.Set<T>().SingleOrDefault(o => o.Id.Equals(id));
        }

        /// <summary>
        /// Returns an instance query by its id
        /// </summary>
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await context.FindAsync<T>(id);
        }

        #endregion Queris

        #region Commands

        /// <summary>
        /// Adds an object
        /// </summary>
        public void Add(T objectToAdd)
        {
            context.Set<T>().Add(objectToAdd);
        }

        #endregion Commands

        #endregion Methods

        #region IQueryable

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator() => SourceQuery.GetEnumerator();

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable"></see> is executed.</summary>
        /// <returns>A <see cref="T:System.Type"></see> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.</returns>
        public Type ElementType => SourceQuery.ElementType;

        /// <summary>Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable"></see>.</summary>
        /// <returns>The <see cref="T:System.Linq.Expressions.Expression"></see> that is associated with this instance of <see cref="T:System.Linq.IQueryable"></see>.</returns>
        public Expression Expression => SourceQuery.Expression;

        /// <summary>Gets the query provider that is associated with this data source.</summary>
        /// <returns>The <see cref="T:System.Linq.IQueryProvider"></see> that is associated with this data source.</returns>
        public IQueryProvider Provider => SourceQuery.Provider;


        #endregion IQueryable

        #region IAsyncEnumerable

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return ((IAsyncEnumerable<T>)SourceQuery).GetAsyncEnumerator(cancellationToken);
        }

        #endregion IAsyncEnumerable
    }
}
