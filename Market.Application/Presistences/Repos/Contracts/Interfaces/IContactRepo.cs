using Market.Application.DTOs.Contact;
using Market.Domain.Entity.HR;
using System.Linq.Expressions;

namespace Market.Application.Presistences.Repos.Contracts.Interfaces;

/// <summary>
/// Defines a repository for managing contact-related data and operations.
/// </summary>
/// <remarks>This interface extends <see cref="IGeneralRepository{T}"/> to provide additional functionality 
/// specific to contacts, such as retrieving contact information associated with a supplier.</remarks>
public interface IContactRepo : IGeneralRepository<Contact>
{
    /// <summary>
    /// Retrieves a collection of contact information associated with the specified supplier.
    /// </summary>
    /// <remarks>This method performs an asynchronous operation to fetch contact details. The returned
    /// collection will be empty  if no contacts are associated with the specified supplier.</remarks>
    /// <param name="PersonID">The unique identifier of the supplier whose contacts are to be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection  of <see
    /// cref="PersonPhoneViewDto"/> objects representing the contact information for the specified supplier.</returns>
    Task<IEnumerable<PersonPhoneViewDto>> GetContactsBySupplierIdAsync(Guid PersonID);

    /// <summary>
    /// Retrieves all contact records as a collection of <see cref="RequestContactDto"/> objects.
    /// </summary>
    /// <typeparam name="Tkey">The type of the key used for ordering the contacts. Typically a property of the <see cref="Contact"/> entity.</typeparam>
    /// <param name="orderBy">An optional expression used to specify the property by which the contacts should be ordered.  If null, the
    /// contacts are returned in their default order.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/>  of
    /// <see cref="RequestContactDto"/> objects representing the retrieved contacts.</returns>
    Task<IEnumerable<RequestContactDto>> GetAllContactAsync<Tkey>(Expression<Func<Contact, Tkey>>? orderBy = null);

    /// <summary>
    /// Retrieves Specific contact records as a collection of <see cref="RequestContactDto"/> objects
    /// </summary>
    /// <param name="ids">List of Identitfiers</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/>  of
    /// <see cref="RequestContactDto"/> objects representing the retrieved contacts</returns>
    Task<List<RequestContactDto>> GetFullSpecificContactsByIdAsync(List<int> ids);
}

