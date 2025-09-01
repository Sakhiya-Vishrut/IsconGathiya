using IsconGathiya.Domain;
using IsconGathiya.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Service.Employee
{
    public interface IEmployeeRepository
    {
        List<EmployeeDTO> GetEmployeeDataWithFilter(int? filterState, int? filterCity, int? filterBranch, int? filtershift, string? EmployeeName, string? staffId, int pageSize, int pageIndex, string columnName, string sortDirection);
        EmployeeDTO GetEmployeeDetails(int? Employeeid);
        EmployeeDTO GetEmployeeViewmodel(int? Employeeid);
        Task<bool> DeleteEmployee(int? EmployeeId);
        Task<bool> AddEditEmployee(EmpEmployee model);
        List<Branch> GetBranchList();
    }
}
