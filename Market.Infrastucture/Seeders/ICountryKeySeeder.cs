
namespace Market.Infrastucture.Seeders;

/// <summary>
/// Defines a contract for seeding country keys into a data store or system.
/// </summary>
/// <remarks>Implementations of this interface are responsible for populating country-related keys in the
/// underlying storage or system. This operation is typically performed during application initialization or data
/// setup.</remarks>
public interface ICountryKeySeeder
{
    /// <summary>
    /// Seeds the database with initial data asynchronously.
    /// </summary>
    /// <remarks>This method is typically called during application startup to ensure the database is
    /// populated with required initial data. It should be invoked only once to avoid duplicate entries or unintended
    /// side effects.</remarks>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SeedAsync();
}