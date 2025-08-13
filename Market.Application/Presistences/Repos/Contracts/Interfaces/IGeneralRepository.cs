using System.Linq.Expressions;

namespace Market.Application.Presistences.Repos.Contracts.Interfaces;

/// <summary>
/// Defines a generic repository interface for performing CRUD operations and querying entities of type <typeparamref
/// name="T"/>.
/// </summary>
/// <remarks>This interface provides methods for common data access operations, including retrieving, adding,
/// updating, and deleting entities. It supports asynchronous operations for improved scalability and performance in
/// data-driven applications.</remarks>
/// <typeparam name="T">The type of the entity managed by the repository. Must be a reference type.</typeparam>
public interface IGeneralRepository<T> where T : class
{
    /// <summary>
    /// Asynchronously retrieves all entities of type <typeparamref name="T"/> from the data source.
    /// </summary>
    /// <typeparam name="Tkey">The type of the key used for ordering the entities.</typeparam>
    /// <param name="orderBy">An optional expression specifying the property to order the entities by.  If <see langword="null"/>, the
    /// entities are returned in their default order.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an  <see cref="IEnumerable{T}"/> of
    /// all entities, ordered if the <paramref name="orderBy"/> parameter is provided.</returns>
    Task<IEnumerable<T>> GetAllAsync<Tkey>(Expression<Func<T, Tkey>>? orderBy = null);

    /// <summary>
    /// Retrieves an entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the matching entity if found.</returns>
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdAsync(short id);
    Task<T?> GetByIdAsync(long id);
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    /// Determines whether an entity with the specified identifier exists asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to check for existence.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains  <see langword="true"/> if the
    /// entity exists; otherwise, <see langword="false"/>.</returns>
    Task<bool> IsExistsAsync(int id);
    Task<bool> IsExistsAsync(short id);
    Task<bool> IsExistsAsync(long id);
    Task<bool> IsExistsAsync(Guid id);

    /// <summary>
    /// Adds a new entity to the data store asynchronously.
    /// </summary>
    /// <param name="entity">The entity to be added</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAsync(T entity);
    Task AddAsync(IEnumerable<T> entities);

    /// <summary>
    /// Deletes the specified entity from the data store.
    /// </summary>
    /// <param name="entity">The entity to be deleted.</param>
    void Delete(T entity);
    void Delete(IEnumerable<T> entities);

    /// <summary>
    /// Updates the specified entity in the data store.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>
    void Update(T entity);
    void Update(IEnumerable<T> entities);
}
