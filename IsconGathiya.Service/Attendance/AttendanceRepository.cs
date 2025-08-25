using IsconGathiya.Common.DependencyInjection;
using IsconGathiya.Domain;
using IsconGathiya.Domain.DataContext;
using IsconGathiya.Domain.DataModels;
using IsconGathiya.Service.Employee;
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
        private readonly  ApplicationDbContext _context;
        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region GetEmployeeDataWithFilter
        public List<EmployeeDTO> GetEmployeeDataWithFilter(int adminBranchId)
        {
            var baseQuery = (from employee in _context.EmpEmployees
                             join bra in _context.Branches on employee.BranchId equals bra.BranchId into branchjoin
                             from branch in branchjoin.DefaultIfEmpty()
                             join states in _context.LocStates on employee.StateId equals states.Id into statejoin
                             from state in statejoin.DefaultIfEmpty()
                             join city in _context.LocCities on employee.CityId equals city.Id into cityJoin
                             from city in cityJoin.DefaultIfEmpty()

                             where employee.BranchId == adminBranchId

                             orderby employee.ModifiedAt descending
                             select new EmployeeDTO
                             {
                                 employee = employee,
                                 branch = branch,
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
    }
}
