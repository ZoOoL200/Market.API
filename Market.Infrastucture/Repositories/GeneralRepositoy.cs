using Market.Application.Presistences.Repos.Contracts.Interfaces;
using Market.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Market.Infrastucture.Repositories;

/// <summary>
/// Provides a generic repository for performing CRUD operations on entities of type <typeparamref name="T"/>.
/// </summary>
/// <remarks>This repository abstracts common data access operations, such as retrieving, adding, updating, and
/// deleting entities. It is designed to work with an Entity Framework Core <see cref="DbSet{TEntity}"/> and supports
/// asynchronous operations.</remarks>
/// <typeparam name="T">The type of the entity managed by the repository. Must be a reference type.</typeparam>
/// <param name="dbcontext"></param>
internal class GeneralRepositoy<T>(AppDbContext dbcontext) : IGeneralRepository<T> where T : class
{
    protected readonly DbSet<T> _dbSet = dbcontext.Set<T>();

    // retrieves all entities of type T from the data source asynchronously with or without order.
    public  async Task<IEnumerable<T>> GetAllAsync<Tkey>(Expression<Func<T, Tkey>>? orderBy = null)

    {
        var query = _dbSet.AsNoTracking().AsQueryable();

        if (orderBy != null)
            query = query.OrderBy(orderBy);

        return await query.ToListAsync();
    }



    // retrieves an entity by its unique identifier asynchronously.
    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
    public async Task<T?> GetByIdAsync(short id) => await _dbSet.FindAsync(id);
    public async Task<T?> GetByIdAsync(long id) => await _dbSet.FindAsync(id);
    public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);



    // Determines whether an entity with the specified identifier exists asynchronously.
    public async Task<bool> IsExistsAsync(int id)
    {
        var keyName = dbcontext.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.Select(p => p.Name).Single();

        if (keyName is null)
            throw new InvalidOperationException($"No primary key found for entity {typeof(T).Name}.");

        return await _dbSet.AsNoTracking().AnyAsync(entity => EF.Property<int>(entity, keyName) == id);
    }

    public async Task<bool> IsExistsAsync(short id)
    {
        var keyName = dbcontext.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.Select(p => p.Name).Single();

        if (keyName is null)
            throw new InvalidOperationException($"No primary key found for entity {typeof(T).Name}.");

        return await _dbSet.AsNoTracking().AnyAsync(entity => EF.Property<short>(entity, keyName) == id);
    }

    public async Task<bool> IsExistsAsync(long id)
    {
        var keyName = dbcontext.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.Select(p => p.Name).Single();

        if (keyName is null)
            throw new InvalidOperationException($"No primary key found for entity {typeof(T).Name}.");

        return await _dbSet.AsNoTracking().AnyAsync(entity => EF.Property<long>(entity, keyName) == id);
    }

    public async Task<bool> IsExistsAsync(Guid id)
    {
        var keyName = dbcontext.Model.FindEntityType(typeof(T))?.FindPrimaryKey()?.Properties.Select(p => p.Name).Single();

        if (keyName is null)
            throw new InvalidOperationException($"No primary key found for entity {typeof(T).Name}.");

        return await _dbSet.AsNoTracking().AnyAsync(entity => EF.Property<Guid>(entity, keyName) == id);
    }



    // Adds a new entity to the data store asynchronously.
    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public async Task AddAsync(IEnumerable<T> entities) => await _dbSet.AddRangeAsync(entities);



    // Deletes the specified entity from the data store.
    public void Delete(T entity) => _dbSet.Remove(entity);
    public void Delete(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);



    // Updates the specified entity in the data store.
    public void Update(T entity) => _dbSet.Update(entity);
    public void Update(IEnumerable<T> entities) => _dbSet.UpdateRange(entities);



}
