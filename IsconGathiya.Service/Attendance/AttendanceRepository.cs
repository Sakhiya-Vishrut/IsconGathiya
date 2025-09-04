// File: Services/AttendanceRepository.cs
using IsconGathiya.Common;
using IsconGathiya.Common.DependencyInjection;
using IsconGathiya.Domain;
using IsconGathiya.Domain.DataContext;
using IsconGathiya.Domain.DataModels;
using IsconGathiya.Service.Employee;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Service.Attendance
{
    [TransientDependency(ServiceType = typeof(IAttendanceRepository))]
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;
        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region GetEmployeeDataWithFilter
        // File: Services/AttendanceRepository.cs
        public List<EmployeeDTO> GetEmployeeDataWithFilter(int adminBranchId, short shift, DateTime attendanceDate)
        {
            var filteredAttendances = _context.EmpAttendances
                .Where(a => a.SignInDate.HasValue && a.SignInDate.Value.Date == attendanceDate.Date);

            var baseQuery = (from employee in _context.EmpEmployees
                             join branch in _context.Branches on employee.BranchId equals branch.BranchId into bj
                             from branch in bj.DefaultIfEmpty()
                             join att in filteredAttendances on employee.EmployeeId equals att.EmployeeId into attj
                             from att in attj.DefaultIfEmpty()
                             where employee.BranchId == adminBranchId && employee.Shift == shift
                             orderby employee.ModifiedAt descending
                             select new EmployeeDTO
                             {
                                 employee = employee,
                                 branch = branch,
                                 attendance = att
                             }).ToList();

            return baseQuery;
        }


        #endregion

        #region GetBranchList
        public List<Branch> GetBranchList()
        {
            var BranchList = (from branch in _context.Branches
                              where branch.DeletedAt == null
                              orderby branch.BranchName ascending
                              select new Branch
                              {
                                  BranchId = branch.BranchId,
                                  BranchName = branch.BranchName,
                              }).ToList();

            return BranchList;
        }
        #endregion

        #region UpdateEmployeeBranch
        public void UpdateEmployeeBranch(int employeeId, int newBranchId, int shift)
        {
            var employee = _context.EmpEmployees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee != null)
            {
                employee.BranchId = newBranchId;
                employee.Shift = (short)shift;
                employee.ModifiedAt = DateTime.Now;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Not found");
            }
        }
        #endregion

        #region AddEditAttendance
        public async Task<bool> AddEditAttendance(List<EmpAttendance> empAttendanceList, bool isBetweenMidnightAnd7AM)
        {
            try
            {
                DateTime currentDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                DateTime yesterdayIST = currentDate.AddDays(-1);

                foreach (var emp in empAttendanceList)
                {
                    if (emp.AttendancId == 0)
                    {
                        emp.SignInDate = isBetweenMidnightAnd7AM ? yesterdayIST.Date : currentDate.Date;
                        emp.CreatedAt = currentDate;
                        emp.ModifiedAt = currentDate;
                        emp.CreatedBy = Guid.Parse("123e4567-e89b-12d3-a456-426614174000");
                        emp.ModifiedBy = Guid.Parse("123e4567-e89b-12d3-a456-426614174000");

                        await _context.EmpAttendances.AddAsync(emp);
                    }
                }
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region IsAttendanceCompleted
        public bool IsAttendanceCompleted(int branchId, DateTime attendanceDate, bool isNightShift)
        {
            if (isNightShift)
            {
                return _context.EmpAttendances
                    .Any(a => a.SignInDate == attendanceDate || a.SignInDate == attendanceDate.AddDays(1));
            }
            return _context.EmpAttendances
                .Any(a => a.SignInDate == attendanceDate);
        } 
        #endregion
    }
}