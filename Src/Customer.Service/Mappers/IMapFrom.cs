using AutoMapper;

namespace CustomerApi.Service.Mappers
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
