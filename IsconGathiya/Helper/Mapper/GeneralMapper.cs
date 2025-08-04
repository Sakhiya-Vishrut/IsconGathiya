using AutoMapper;

namespace IsconGathiya.Helper.Mapper
{
    public static class GeneralMapper
    {
        /// <summary>
        /// General method to map a source class to destination class
        /// </summary>
        /// <typeparam name="TDestination">destination class</typeparam>
        /// <param name="source">source class</param>
        /// <returns>return mapped destination object</returns>
        public static TDestination ToModel<TDestination>(this object source)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap(source.GetType(), typeof(TDestination)));
            IMapper mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// Map a list of object from source to destination
        /// </summary>
        /// <typeparam name="TDestination">destination object</typeparam>
        /// <typeparam name="TSource">source object</typeparam>
        /// <param name="source">source object</param>
        /// <returns>return mapped destination list of object</returns>
        public static List<TDestination> ToModelList<TDestination, TSource>(this List<TSource> source)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap(typeof(TSource), typeof(TDestination)));
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<TDestination>>(source);
        }
    }
}
