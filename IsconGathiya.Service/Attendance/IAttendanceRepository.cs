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
        List<EmployeeDTO> GetEmployeeDataWithFilter(int adminBranchId, short shift);

        List<Branch> GetBranchList();

        void UpdateEmployeeBranch(int employeeId, int newBranchId);
    }
}
