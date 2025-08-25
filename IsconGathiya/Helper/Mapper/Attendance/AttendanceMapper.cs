using AutoMapper;
using IsconGathiya.Domain;
using static IsconGathiya.ViewModel.EmployeeViewModel;

namespace IsconGathiya.Helper.Mapper.Attendance
{
    public static class AttendanceMapper
    {
        public static List<EmployeeDetails> ToModel(this List<EmployeeDTO> entity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeDTO, EmployeeDetails>()
                    .ForMember(dest => dest.EmployeeId, mo => mo.MapFrom(src => src.employee.EmployeeId))
                    .ForMember(dest => dest.BranchId, mo => mo.MapFrom(src => src.employee.BranchId))
                    .ForMember(dest => dest.BranchName, mo => mo.MapFrom(src => src.branch.BranchName))
                    .ForMember(dest => dest.EmployeeName, mo => mo.MapFrom(src => src.employee.EmployeeName))
                    .ForMember(dest => dest.StaffId, mo => mo.MapFrom(src => src.employee.StaffId))
                    .ForMember(dest => dest.IsActive, mo => mo.MapFrom(src => src.employee.IsActive))
                    .ForMember(dest => dest.Shift, mo => mo.MapFrom(src => src.employee.Shift))
                    .ForMember(dest => dest.Shift, mo => mo.MapFrom(src => (short)src.employee.Shift));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<EmployeeDTO>, List<EmployeeDetails>>(entity);
        }
    }
}
