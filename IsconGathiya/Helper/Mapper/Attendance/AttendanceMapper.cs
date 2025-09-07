// File: Mappers/AttendanceMapper.cs
using AutoMapper;
using IsconGathiya.Domain;
using IsconGathiya.Domain.DataModels;
using static IsconGathiya.ViewModel.AttendanceViewModel;
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
                    .ForMember(dest => dest.Shift, mo => mo.MapFrom(src => (short)src.employee.Shift))
                    .ForMember(dest => dest.AttendanceId, mo => mo.MapFrom(src => src.attendance != null ? src.attendance.AttendancId : (int?)null))
                    .ForMember(dest => dest.AttendanceStatus, mo => mo.MapFrom(src => src.attendance != null ? src.attendance.Status : (short?)null));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<EmployeeDTO>, List<EmployeeDetails>>(entity);
        }
        public static List<AttendanceDetails> ToModel(this List<AttandenceDTO> entity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AttandenceDTO, AttendanceDetails>()
                    .ForMember(dest => dest.EmployeeId, mo => mo.MapFrom(src => src.employee.EmployeeId))
                    .ForMember(dest => dest.AttendancId, mo => mo.MapFrom(src => src.EmpAttendanc.AttendancId))
                    .ForMember(dest => dest.SignInDate, mo => mo.MapFrom(src => src.EmpAttendanc.SignInDate))
                    .ForMember(dest => dest.SignoutDate, mo => mo.MapFrom(src => src.EmpAttendanc.SignoutDate))
                    .ForMember(dest => dest.Status, mo => mo.MapFrom(src => src.EmpAttendanc.Status))
                    .ForMember(dest => dest.Reason, mo => mo.MapFrom(src => src.EmpAttendanc.Reason));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<AttandenceDTO>, List<AttendanceDetails>>(entity);
        }

        public static List<EmpAttendance> ToModel(this List<AttendanceDetails> entity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AttendanceDetails, EmpAttendance>()
                    .ForMember(dest => dest.EmployeeId, mo => mo.MapFrom(src => src.EmployeeId))
                    .ForMember(dest => dest.AttendancId, mo => mo.MapFrom(src => src.AttendancId))
                    .ForMember(dest => dest.SignInDate, mo => mo.MapFrom(src => src.SignInDate))
                    .ForMember(dest => dest.SignoutDate, mo => mo.MapFrom(src => src.SignoutDate))
                    .ForMember(dest => dest.Status, mo => mo.MapFrom(src => src.Status))
                    .ForMember(dest => dest.Status, mo => mo.MapFrom(src => (short)src.Status))
                    .ForMember(dest => dest.Reason, mo => mo.MapFrom(src => src.Reason));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<AttendanceDetails>, List<EmpAttendance>>(entity);
        }
    }
}