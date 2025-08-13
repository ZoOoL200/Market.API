using Microsoft.Extensions.DependencyInjection;

namespace Market.Infrastucture.Extension;

internal class LazyResolver<T>(IServiceProvider provider) : Lazy<T>(() => provider.GetRequiredService<T>()) where T : class
{
}
