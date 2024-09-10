using AutoMapper;

namespace FileProcessingApp.Common.Mappings
{
    public static class MapperProvider
    {
        public static IMapper Mapper { get; private set; }

        public static void Initialize(IMapper mapper)
        {
            Mapper = mapper;
        }
    }

}
