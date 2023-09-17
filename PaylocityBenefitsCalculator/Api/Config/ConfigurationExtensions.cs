using AutoMapper;

namespace Api.Config;

public static class ConfigurationExtensions
{
    /// <summary>
    /// Service collection extension to add Auto mapper.
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public static IServiceCollection AddAutoMapper(this IServiceCollection serviceCollection) => serviceCollection.AddSingleton<IMapper>(provider =>
    {
        MapperConfiguration mapperConfiguration = new MapperConfiguration((Action<IMapperConfigurationExpression>) (cfg =>
        {
            cfg.AddMaps(typeof (Program));
            cfg.ConstructServicesUsing((Func<Type, object>) (type => ActivatorUtilities.CreateInstance(provider, type)));
        }));
        mapperConfiguration.AssertConfigurationIsValid();
        return mapperConfiguration.CreateMapper();
    });
}