using Market.Application.Presistences.Repos.Contracts.Interfaces;
using Market.Domain.Entity.HR;
using Market.Domain.Entity.Management;

namespace Market.Application.Presistences.UnitofWork;
/// <summary>
/// Defines a unit of work that encapsulates a set of operations to be performed as a single transaction.
/// </summary>
/// <remarks>The <see cref="IUnitofWork"/> interface provides a mechanism for coordinating changes across multiple
/// repositories and ensuring that these changes are committed or rolled back as a single unit. It includes methods for
/// managing transactions, saving changes, and accessing repositories. Implementations of this interface should ensure
/// proper resource management, including disposing of resources when the unit of work is no longer needed.</remarks>
public interface IUnitofWork : IDisposable
{
    /// <summary>
    /// Gets the repository used for managing contact data.
    /// </summary>
    public IContactRepo ContactRepo { get; }

    public IGeneralRepository<CountryKey> CountryKeyRepo { get; }
    /// <summary>
    /// Gets the repository for managing <see cref="Supplier"/> entities.
    /// </summary>
    public IGeneralRepository<Supplier> SupplierRepo { get; }

    /// <summary>
    /// Asynchronously saves all changes made in the current context to the database.
    /// </summary>
    /// <remarks>This method commits all tracked changes to the underlying database. If any validation or
    /// concurrency issues occur, an exception will be thrown.</remarks>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the save operation.</param>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries
    /// written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Saves all changes made in the current context to the underlying database.
    /// </summary>
    /// <remarks>This method asynchronously commits all tracked changes to the database.  It returns the
    /// number of state entries written to the database.  If no changes are detected, the method returns 0.</remarks>
    /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries
    /// written to the database.</returns>
    Task<int> SaveChanges();
    /// <summary>
    /// Begins a new database transaction asynchronously.
    /// </summary>
    /// <remarks>Use this method to initiate a transaction for executing multiple database operations as a
    /// single unit of work. Ensure that the transaction is committed or rolled back to finalize or discard the
    /// changes.</remarks>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>A task that represents the asynchronous operation. The task completes when the transaction has been successfully
    /// started.</returns>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Commits the current transaction asynchronously.
    /// </summary>
    /// <remarks>This method finalizes the transaction, making all changes within the transaction permanent.
    /// If the transaction cannot be committed, an exception will be thrown.</remarks>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the commit operation.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Asynchronously rolls back the current transaction, undoing all changes made during the transaction.
    /// </summary>
    /// <remarks>This method should be called to revert changes when a transaction cannot be successfully
    /// committed. Ensure that the transaction is still active before calling this method, as calling it on a completed
    /// or disposed transaction may result in an exception.</remarks>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. The operation will be canceled if the token is triggered.</param>
    /// <returns>A task that represents the asynchronous rollback operation.</returns>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
