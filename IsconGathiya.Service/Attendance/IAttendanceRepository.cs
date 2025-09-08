// File: Services/IAttendanceRepository.cs
using IsconGathiya.Domain;
using IsconGathiya.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Service.Attendance
{
    public interface IAttendanceRepository
    {
        List<EmployeeDTO> GetEmployeeDataWithFilter(int adminBranchId, short shift, DateTime attendanceDate);

        List<AttandenceDTO> GetEmployeeAttandenceDataWithFilter(int branchId, short shiftType, DateTime attendanceDate);

        List<AttandenceDTO> GetEmployeeAttandenceAbsentDataWithFilter(int branchId, short shiftType, DateTime attendanceDate);

        List<Branch> GetBranchList();

        Task<bool> SignoutDetails(EmpAttendance model, int userId);

        Task<AttandenceDTO> GetAttendanceById(int attendanceId);

        Task<bool> UpdateReason(EmpAttendance empAttendance, int adminId);

        void UpdateEmployeeBranch(int employeeId, int newBranchId, int shift);

        Task<bool> AddEditAttendance(List<EmpAttendance> empAttendanceList, bool isBetweenMidnightAnd7AM);

        bool IsAttendanceCompleted(int branchId, DateTime attendanceDate, bool isNightShift);
    }
}