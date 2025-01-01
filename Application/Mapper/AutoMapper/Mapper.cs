using Application.Interface.AutoMapper;
using AutoMapper;
using AutoMapper.Internal;


namespace Application.Mapper.AutoMapper;

public class Mapper:ICustomMapper
{
    
    public static List<TypePair> typePairs = new();
    private IMapper MapperContainer;
    
    public TDestination Map<TDestination, TSource>(TSource source, string? ignoreNull = null)
    {
        Config<TDestination, TSource>(5, ignoreNull);
            return MapperContainer.Map<TSource,TDestination>(source);
    }

    public IList<TDestination> Map<TDestination, TSource>(IList<TSource> source, string? ignoreNull = null)
    {
        Config<IList<TDestination>, List<TSource>>(5, ignoreNull);
        return MapperContainer.Map<IList<TSource>, IList<TDestination>>(source);
    }

    public TDestination Map<TDestination>(object source, string? ignoreNull = null)
    {
        Config<TDestination, object>(5, ignoreNull);
        return MapperContainer.Map<TDestination>(source);
    }

    public IList<TDestination> Map<TDestination>(IEnumerable<object> source, string? ignoreNull = null)
    {
        Config<TDestination, IList<object>>(5, ignoreNull);
        return MapperContainer.Map<IList<TDestination>>(source);
    }

    protected void Config<TDestination, TSource>(int depth = 5, string? ignoreNull = null)
    {
        // Yeni TypePair oluştur ve zaten mevcutsa geri dön
        var typePair = new TypePair(typeof(TDestination), typeof(TSource));
        if (typePairs.Any(a => a.DestinationType == typePair.DestinationType 
                               && a.SourceType == typePair.SourceType && ignoreNull is null))
        {
            return;
        }

        // Listeye ekle
        typePairs.Add(typePair);

        // MapperConfiguration yapılandırması
        var config = new MapperConfiguration(cfg =>
        {
            foreach (var pair in typePairs) // typePair yerine 'pair' kullanımı çakışmayı önler
            {
                // ignoreNull kontrolü
                if (!string.IsNullOrEmpty(ignoreNull))
                {
                    cfg.CreateMap(pair.SourceType, pair.DestinationType)
                        .MaxDepth(depth)
                        .ForMember(ignoreNull, opt => opt.Ignore())
                        .ReverseMap();
                }
                else
                {
                    cfg.CreateMap(pair.SourceType, pair.DestinationType)
                        .MaxDepth(depth)
                        .ReverseMap();
                }
            }
        });

        MapperContainer = config.CreateMapper();
    }
}