using Market.Application.DTOs.Contact;
using Market.Application.Presistences.Repos.Contracts.Interfaces;
using Market.Domain.Entity.HR;
using Market.Infrastucture.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Market.Infrastucture.Repositories;
/// <summary>
/// Provides data access methods for managing contacts in the database.
/// </summary>
/// <remarks>This repository is responsible for retrieving and managing contact-related data,  including
/// operations specific to contacts associated with a supplier.</remarks>
/// <param name="context"></param>
internal class ContactRepo(AppDbContext context) : GeneralRepositoy<Contact>(context), IContactRepo
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<PersonPhoneViewDto>> GetContactsBySupplierIdAsync(Guid PersonID)
        => await (from contact in _context.Contacts
                  join key in _context.CountryKeys on contact.CountryID equals key.Id
                  where contact.PersonID == PersonID
                  select new PersonPhoneViewDto
                  {
                      CountryName = key.CountryName,
                      Key = key.Key,
                      Telephone = contact.Telephone
                  }).ToListAsync();

    public  async Task<IEnumerable<RequestContactDto>> GetAllContactAsync<Tkey>(Expression<Func<Contact, Tkey>>? orderBy = null)
    {
        var query = _dbSet.AsNoTracking().AsQueryable();
        if (orderBy != null)
            query = query.OrderBy(orderBy);
        return await query.Select(c=> new RequestContactDto 
        {
            Id= c.Id,
            PersonName =c.Supplier.SupplierName ,
            CountryName = c.Country.CountryName,
            Key= c.Country.Key,
            Telephone= c.Telephone
        }).ToListAsync();
    }

    public async Task<List<RequestContactDto>> GetFullSpecificContactsByIdAsync(List<int> ids)
    {
        return await _dbSet.AsNoTracking()
        .Where(c => ids.Contains(c.Id))
        .Select(c => new RequestContactDto
        {
            Id = c.Id,
            PersonName = c.Supplier.SupplierName,
            CountryName = c.Country.CountryName,
            Key = c.Country.Key,
            Telephone = c.Telephone
        }).ToListAsync();

    }
}
