using AutoMapper;
using IsconGathiya.Domain;
using IsconGathiya.Domain.DataModels;
using static IsconGathiya.ViewModel.BranchViewModel;

namespace IsconGathiya.Helper.Mapper.BranchMapper
{
    public static class BranchMapper
    {
        public static List<BranchDetails> ToModel(this List<BranchDTO> entity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<BranchDTO, BranchDetails>()
                    .ForMember(dest => dest.BranchId, mo => mo.MapFrom(src => src.branch.BranchId))
                    .ForMember(dest => dest.StateId, mo => mo.MapFrom(src => src.branch.StateId))
                    .ForMember(dest => dest.CityId, mo => mo.MapFrom(src => src.branch.CityId))
                    .ForMember(dest => dest.BranchName, mo => mo.MapFrom(src => src.branch.BranchName))
                    .ForMember(dest => dest.Address, mo => mo.MapFrom(src => src.branch.Address));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<BranchDTO>, List<BranchDetails>>(entity);
        }

        public static BranchDetails ToModel(this BranchDTO entity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<BranchDTO, BranchDetails>()
                    .ForMember(dest => dest.BranchId, mo => mo.MapFrom(src => src.branch.BranchId))
                    .ForMember(dest => dest.StateId, mo => mo.MapFrom(src => src.branch.StateId))
                    .ForMember(dest => dest.CityId, mo => mo.MapFrom(src => src.branch.CityId))
                    .ForMember(dest => dest.BranchName, mo => mo.MapFrom(src => src.branch.BranchName))
                    .ForMember(dest => dest.CountryId, mo => mo.MapFrom(src => src.branch.CountryId))
                    .ForMember(dest => dest.CountryName, mo => mo.MapFrom(src => src.LocCountry.Name))
                    .ForMember(dest => dest.StateId, mo => mo.MapFrom(src => src.branch.StateId))
                    .ForMember(dest => dest.StateName, mo => mo.MapFrom(src => src.LocState.Name))
                    .ForMember(dest => dest.CityId, mo => mo.MapFrom(src => src.branch.CityId))
                    .ForMember(dest => dest.CityName, mo => mo.MapFrom(src => src.LocCity.Name))
                    .ForMember(dest => dest.Address, mo => mo.MapFrom(src => src.branch.Address));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<BranchDTO, BranchDetails>(entity);
        }

        public static Branch ToModel(this BranchDetails entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BranchDetails, Branch>()
                    .ForMember(dest => dest.BranchId, mo => mo.MapFrom(src => src.BranchId))
                    .ForMember(dest => dest.BranchName, mo => mo.MapFrom(src => src.BranchName))
                    .ForMember(dest => dest.StateId, mo => mo.MapFrom(src => src.StateId))
                    .ForMember(dest => dest.CityId, mo => mo.MapFrom(src => src.CityId))
                    .ForMember(dest => dest.Address, mo => mo.MapFrom(src => src.Address));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<BranchDetails, Branch>(entity);
        }
    }
}
