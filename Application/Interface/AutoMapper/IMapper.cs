namespace Application.Interface.AutoMapper;

public interface ICustomMapper
{
    TDestination Map<TDestination,TSource>(TSource source,string? ignoreNull = null); 
    IList<TDestination> Map<TDestination,TSource>(IList<TSource> source,string? ignoreNull = null);
    TDestination Map<TDestination>(object source,string? ignoreNull = null);
    IList<TDestination> Map<TDestination>(IEnumerable<object> source,string? ignoreNull = null);
}