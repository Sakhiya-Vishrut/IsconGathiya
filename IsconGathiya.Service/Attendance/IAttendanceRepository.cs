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
        List<EmployeeDTO> GetEmployeeDataWithFilter(int adminBranchId);

        List<Branch> GetBranchList();

    }
}
