using AutoMapper;
using IsconGathiya.Common;
using IsconGathiya.Domain;
using IsconGathiya.Domain.DataModels;
using static IsconGathiya.Common.Enums;
using static IsconGathiya.ViewModel.EmployeeViewModel;

namespace IsconGathiya.Helper.Mapper.Employee
{
    public static class EmployeeMapper
    {
        public static List<EmployeeDetails> ToModel(this List<EmployeeDTO> entity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeDTO, EmployeeDetails>()
                    .ForMember(dest => dest.EmployeeId, mo => mo.MapFrom(src => src.employee.EmployeeId))
                    .ForMember(dest => dest.BranchId, mo => mo.MapFrom(src => src.employee.BranchId))
                    .ForMember(dest => dest.StateId, mo => mo.MapFrom(src => src.employee.StateId))
                    .ForMember(dest => dest.CityId, mo => mo.MapFrom(src => src.employee.CityId))
                    .ForMember(dest => dest.BranchName, mo => mo.MapFrom(src => src.branch.BranchName))
                    .ForMember(dest => dest.CityName, mo => mo.MapFrom(src => src.city.Name))
                    .ForMember(dest => dest.StateName, mo => mo.MapFrom(src => src.state.Name))
                    .ForMember(dest => dest.EmployeeName, mo => mo.MapFrom(src => src.employee.EmployeeName))
                    .ForMember(dest => dest.StaffId, mo => mo.MapFrom(src => src.employee.StaffId))
                    .ForMember(dest => dest.MobileNumber, mo => mo.MapFrom(src => src.employee.MobileNumber))
                    .ForMember(dest => dest.FamilyNumber, mo => mo.MapFrom(src => src.employee.FamilyNumber))
                    .ForMember(dest => dest.FamilyMemberName, mo => mo.MapFrom(src => src.employee.FamilyMemberName))
                    .ForMember(dest => dest.WhatsAppNumber, mo => mo.MapFrom(src => src.employee.WhatsAppNumber))
                    .ForMember(dest => dest.BirthDate, mo => mo.MapFrom(src => src.employee.BirthDate))
                    .ForMember(dest => dest.Email, mo => mo.MapFrom(src => src.employee.Email))
                    .ForMember(dest => dest.AlternateNumber, mo => mo.MapFrom(src => src.employee.AlternateNumber))
                    .ForMember(dest => dest.Address, mo => mo.MapFrom(src => src.employee.Address))
                    .ForMember(dest => dest.Gender, mo => mo.MapFrom(src => src.employee.Gender))
                    .ForMember(dest => dest.Gender, mo => mo.MapFrom(src => (short)src.employee.Gender))
                    .ForMember(dest => dest.Salary, mo => mo.MapFrom(src => src.employee.Salary))
                    .ForMember(dest => dest.IsActive, mo => mo.MapFrom(src => src.employee.IsActive))
                    .ForMember(dest => dest.JoiningDate, mo => mo.MapFrom(src => src.employee.JoiningDate))
                    .ForMember(dest => dest.EmployeeType, mo => mo.MapFrom(src => src.employee.EmployeeType))
                    .ForMember(dest => dest.EmployeeType, mo => mo.MapFrom(src => (short)src.employee.EmployeeType)) 
                    .ForMember(dest => dest.AdharNo, mo => mo.MapFrom(src => src.employee.AdharNo))
                    .ForMember(dest => dest.Shift, mo => mo.MapFrom(src => src.employee.Shift))
                    .ForMember(dest => dest.Shift, mo => mo.MapFrom(src => (short)src.employee.Shift))
                    .ForMember(dest => dest.Education, mo => mo.MapFrom(src => src.employee.Education));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<EmployeeDTO>, List<EmployeeDetails>>(entity);
        }

        public static EmployeeDetails ToModel(this EmployeeDTO entity)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<EmployeeDTO, EmployeeDetails>()
                    .ForMember(dest => dest.EmployeeId, mo => mo.MapFrom(src => src.employee.EmployeeId))
                    .ForMember(dest => dest.BranchId, mo => mo.MapFrom(src => src.employee.BranchId))
                    .ForMember(dest => dest.StateId, mo => mo.MapFrom(src => src.employee.StateId))
                    .ForMember(dest => dest.CityId, mo => mo.MapFrom(src => src.employee.CityId))
                    .ForMember(dest => dest.BranchName, mo => mo.MapFrom(src => src.branch.BranchName))
                    .ForMember(dest => dest.CityName, mo => mo.MapFrom(src => src.city.Name))
                    .ForMember(dest => dest.StateName, mo => mo.MapFrom(src => src.state.Name))
                    .ForMember(dest => dest.EmployeeName, mo => mo.MapFrom(src => src.employee.EmployeeName))
                    .ForMember(dest => dest.StaffId, mo => mo.MapFrom(src => src.employee.StaffId))
                    .ForMember(dest => dest.MobileNumber, mo => mo.MapFrom(src => src.employee.MobileNumber))
                    .ForMember(dest => dest.WhatsAppNumber, mo => mo.MapFrom(src => src.employee.WhatsAppNumber))
                    .ForMember(dest => dest.BirthDate, mo => mo.MapFrom(src => src.employee.BirthDate))
                    .ForMember(dest => dest.Email, mo => mo.MapFrom(src => src.employee.Email))
                    .ForMember(dest => dest.FamilyMemberName, mo => mo.MapFrom(src => src.employee.FamilyMemberName))
                    .ForMember(dest => dest.FamilyNumber, mo => mo.MapFrom(src => src.employee.FamilyNumber))
                    .ForMember(dest => dest.AlternateNumber, mo => mo.MapFrom(src => src.employee.AlternateNumber))
                    .ForMember(dest => dest.Address, mo => mo.MapFrom(src => src.employee.Address))
                    .ForMember(dest => dest.Gender, mo => mo.MapFrom(src => src.employee.Gender))
                    .ForMember(dest => dest.Gender, mo => mo.MapFrom(src => (short)src.employee.Gender))
                    .ForMember(dest => dest.FamilyNumber, mo => mo.MapFrom(src => src.employee.FamilyNumber))
                    .ForMember(dest => dest.FamilyMemberName, mo => mo.MapFrom(src => src.employee.FamilyMemberName))
                    .ForMember(dest => dest.IsActive, mo => mo.MapFrom(src => src.employee.IsActive))
                    .ForMember(dest => dest.Salary, mo => mo.MapFrom(src => src.employee.Salary))
                    .ForMember(dest => dest.JoiningDate, mo => mo.MapFrom(src => src.employee.JoiningDate))
                    .ForMember(dest => dest.EmployeeType, mo => mo.MapFrom(src => src.employee.EmployeeType))
                    .ForMember(dest => dest.EmployeeType, mo => mo.MapFrom(src => (short)src.employee.EmployeeType))
                    .ForMember(dest => dest.AdharNo, mo => mo.MapFrom(src => src.employee.AdharNo))
                    .ForMember(dest => dest.Shift, mo => mo.MapFrom(src => src.employee.Shift))
                    .ForMember(dest => dest.Shift, mo => mo.MapFrom(src => (short)src.employee.Shift))
                    .ForMember(dest => dest.Education, mo => mo.MapFrom(src => src.employee.Education));

            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<EmployeeDTO, EmployeeDetails>(entity);
        }

        public static EmpEmployee ToModel(this EmployeeDetails entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeDetails, EmpEmployee>()
                    .ForMember(dest => dest.EmployeeId, mo => mo.MapFrom(src => src.EmployeeId))
                    .ForMember(dest => dest.BranchId, mo => mo.MapFrom(src => src.BranchId))
                    .ForMember(dest => dest.StateId, mo => mo.MapFrom(src => src.StateId))
                    .ForMember(dest => dest.CityId, mo => mo.MapFrom(src => src.CityId))
                    .ForMember(dest => dest.EmployeeName, mo => mo.MapFrom(src => src.EmployeeName))
                    .ForMember(dest => dest.StaffId, mo => mo.MapFrom(src => src.StaffId))
                    .ForMember(dest => dest.MobileNumber, mo => mo.MapFrom(src => src.MobileNumber))
                    .ForMember(dest => dest.WhatsAppNumber, mo => mo.MapFrom(src => src.WhatsAppNumber))
                    .ForMember(dest => dest.BirthDate, mo => mo.MapFrom(src => src.BirthDate))
                    .ForMember(dest => dest.Email, mo => mo.MapFrom(src => src.Email))
                    .ForMember(dest => dest.AlternateNumber, mo => mo.MapFrom(src => src.AlternateNumber))
                    .ForMember(dest => dest.Address, mo => mo.MapFrom(src => src.Address))
                    .ForMember(dest => dest.FamilyNumber, mo => mo.MapFrom(src => src.FamilyNumber))
                    .ForMember(dest => dest.FamilyMemberName, mo => mo.MapFrom(src => src.FamilyMemberName))
                    .ForMember(dest => dest.Gender, mo => mo.MapFrom(src => src.Gender))
                    .ForMember(dest => dest.Gender, mo => mo.MapFrom(src => src.Gender ?? Enums.GetEnumValue<Gender>(src.GenderType)))
                    .ForMember(dest => dest.Salary, mo => mo.MapFrom(src => src.Salary))
                    .ForMember(dest => dest.IsActive, mo => mo.MapFrom(src => src.IsActive))
                    .ForMember(dest => dest.JoiningDate, mo => mo.MapFrom(src => src.JoiningDate))
                    .ForMember(dest => dest.EmployeeType, mo => mo.MapFrom(src => src.EmployeeType))
                    .ForMember(x => x.Shift, mo => mo.MapFrom(x => Enums.GetEnumDescription<Shift>(x.Shift.GetValueOrDefault())))
                    .ForMember(dest => dest.EmployeeType, mo => mo.MapFrom(src => src.EmployeeType ?? Enums.GetEnumValue<EmployeeType>(src.EmployeeTypeName)))
                    .ForMember(dest => dest.AdharNo, mo => mo.MapFrom(src => src.AdharNo))
                    .ForMember(dest => dest.Shift, mo => mo.MapFrom(src => src.Shift))
                    .ForMember(dest => dest.Shift, mo => mo.MapFrom(src => src.Shift ?? Enums.GetEnumValue<Shift>(src.ShiftType)))
                    .ForMember(dest => dest.Education, mo => mo.MapFrom(src => src.Education));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<EmployeeDetails, EmpEmployee>(entity);
        }

    }
}
