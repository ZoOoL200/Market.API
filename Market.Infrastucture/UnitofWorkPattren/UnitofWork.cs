using Market.Application.Presistences.Repos.Contracts.Interfaces;
using Market.Application.Presistences.UnitofWork;
using Market.Domain.Entity.HR;
using Market.Domain.Entity.Management;
using Market.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace Market.Infrastucture.UnitofWorkPattren;

internal class UnitofWork(AppDbContext context, Lazy<IContactRepo> contactRepo , Lazy<IGeneralRepository<CountryKey>> countryKeyRepo ,
    Lazy<IGeneralRepository<Supplier>> supplierRepo) : IUnitofWork
{
    private IDbContextTransaction? transaction;




    // Rrpositories
    public IContactRepo ContactRepo => contactRepo.Value;

    public IGeneralRepository<CountryKey> CountryKeyRepo => countryKeyRepo.Value;
    public IGeneralRepository<Supplier> SupplierRepo => supplierRepo.Value;





    //Methods
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) 
    {
        transaction = await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (transaction != null)
        {
            await transaction.CommitAsync(cancellationToken);
            await transaction.DisposeAsync();
            transaction = null;
        }
    }

    public void Dispose()
    {
        context.Dispose();
        transaction?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) 
    {
        if (transaction != null)
        {
            await transaction.RollbackAsync(cancellationToken);
            await transaction.DisposeAsync();
            transaction = null;
        }
    }

    public async Task<int> SaveChanges() => await context.SaveChangesAsync();


    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
