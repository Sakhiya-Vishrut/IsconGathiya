using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using IsconGathiya.Common.DependencyInjection;
using IsconGathiya.Domain;
using IsconGathiya.Domain.DataContext;
using IsconGathiya.Domain.DataModels;
using Microsoft.EntityFrameworkCore;

namespace IsconGathiya.Service.Employee
{
    [TransientDependency(ServiceType = typeof(IEmployeeRepository))]
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region GetEmployeeDataWithFilter
        public List<EmployeeDTO> GetEmployeeDataWithFilter(int? filterState, int? filterCity, int? filterBranch, int? filtershift, string? EmployeeName, string? stafId, int pageSize, int pageIndex, string columnName, string sortDirection)
        {
            var baseQuery = (from employee in _context.EmpEmployees
                             join bra in _context.Branches on employee.BranchId equals bra.BranchId into branchjoin
                             from branch in branchjoin.DefaultIfEmpty()
                             join states in _context.LocStates on employee.StateId equals states.Id into statejoin
                             from state in statejoin.DefaultIfEmpty()
                             join city in _context.LocCities on employee.CityId equals city.Id into cityJoin
                             from city in cityJoin.DefaultIfEmpty()
                             where (employee.DeletedAt == null)
                                && (!filterState.HasValue || state.Id == filterState.Value)
                                && (!filterCity.HasValue || city.Id == filterCity.Value)
                                && (!filterBranch.HasValue || branch.BranchId == filterBranch.Value)
                                && (!filtershift.HasValue || employee.Shift == filtershift.Value)
                                && (string.IsNullOrEmpty(EmployeeName) || employee.EmployeeName.ToLower().Contains(EmployeeName.ToLower()))
                                && (string.IsNullOrEmpty(stafId) || employee.StaffId.ToLower().Contains(stafId.ToLower()))

                             orderby employee.ModifiedAt descending
                             select new EmployeeDTO
                             {
                                 employee = employee,
                                 branch = branch,
                             }).ToList();


            switch (columnName)
            {
                case "Branch":
                    baseQuery = sortDirection == "asc"
                        ? baseQuery.OrderBy(a => a.employee.BranchId).ToList()
                        : baseQuery.OrderByDescending(a => a.employee.BranchId).ToList();
                    break;
                case "EmployeeName":
                    baseQuery = sortDirection == "asc"
                        ? baseQuery.OrderBy(a => a.employee.EmployeeName).ToList()
                        : baseQuery.OrderByDescending(a => a.employee.EmployeeName).ToList();
                    break;
                case "ContactNumber":
                    baseQuery = sortDirection == "asc"
                         ? baseQuery.OrderBy(a => a.employee.MobileNumber).ToList()
                         : baseQuery.OrderByDescending(a => a.employee.MobileNumber).ToList();
                    break;
                case "Shift":
                    baseQuery = sortDirection == "asc"
                          ? baseQuery.OrderBy(a => a.employee.Shift).ToList()
                          : baseQuery.OrderByDescending(a => a.employee.Shift).ToList();
                    break;

                case "IsActive":
                    baseQuery = sortDirection == "asc"
                          ? baseQuery.OrderBy(a => a.employee.IsActive).ToList()
                          : baseQuery.OrderByDescending(a => a.employee.IsActive).ToList();
                    break;
                default:
                    baseQuery = sortDirection == "asc"
                          ? baseQuery.OrderBy(a => a.employee.ModifiedAt).ToList()
                          : baseQuery.OrderByDescending(a => a.employee.ModifiedAt).ToList();
                    break;
            }

            int count = 0;

            if (baseQuery.Count() > 0)
                count = baseQuery.Count();

            baseQuery = baseQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            if (baseQuery.Count > 0)
                baseQuery[0].TotalRecords = count;

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

        #region GetEmployeeDetails
        public EmployeeDTO GetEmployeeDetails(int? Employeeid)
        {
            var employee = _context.EmpEmployees
                .Where(p => p.EmployeeId == Employeeid && p.DeletedAt == null)
                .Select(p => new EmployeeDTO
                {
                    employee = p
                }).FirstOrDefault();
            return employee;
        }
        #endregion

        #region GetEmployeeViewmodel
        public EmployeeDTO GetEmployeeViewmodel(int? Employeeid)
        {
            var emp = (from employee in _context.EmpEmployees
                       join bra in _context.Branches on employee.BranchId equals bra.BranchId into branchjoin
                       from branch in branchjoin.DefaultIfEmpty()
                       join states in _context.LocStates on employee.StateId equals states.Id into statejoin
                       from state in statejoin.DefaultIfEmpty()
                       join city in _context.LocCities on employee.CityId equals city.Id into cityJoin
                       from city in cityJoin.DefaultIfEmpty()
                       where (employee.DeletedAt == null || branch.DeletedAt == null) && employee.EmployeeId == Employeeid
                       orderby employee.ModifiedAt descending
                       select new EmployeeDTO
                       {
                           employee = employee,
                           state = state,
                           city = city,
                           branch = branch,
                       }).FirstOrDefault();

            return emp;
        }
        #endregion

        #region DeleteEmployee
        public async Task<bool> DeleteEmployee(int? EmployeeId)
        {
            try
            {
                var existingEmmployee = await _context.EmpEmployees.Where(employee => employee.EmployeeId == EmployeeId && employee.DeletedAt == null).FirstOrDefaultAsync();
                if (existingEmmployee != null)
                {
                    existingEmmployee.DeletedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    _context.EmpEmployees.Update(existingEmmployee);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region AddEditEmployee
        public async Task<bool> AddEditEmployee(EmpEmployee model)
        {
            try
            {
                DateTime CurrentDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                if (model.EmployeeId == 0)
                {
                    model.CreatedAt = CurrentDate;
                    model.ModifiedAt = CurrentDate;
                    model.CreatedBy = 1;
                    model.ModifiedBy = 1;
                    await _context.EmpEmployees.AddAsync(model);
                }
                else
                {
                    var existingEmployee = await _context.EmpEmployees.FirstOrDefaultAsync(p => p.EmployeeId == model.EmployeeId);

                    if (existingEmployee == null)
                        return false;

                    existingEmployee.BranchId = model.BranchId;
                    existingEmployee.CityId = model.CityId;
                    existingEmployee.StateId = model.StateId;
                    existingEmployee.EmployeeName = model.EmployeeName;
                    existingEmployee.StaffId = model.StaffId;
                    existingEmployee.MobileNumber = model.MobileNumber;
                    existingEmployee.WhatsAppNumber = model.WhatsAppNumber;
                    existingEmployee.BirthDate = model.BirthDate;
                    existingEmployee.Email = model.Email;
                    existingEmployee.AlternateNumber = model.AlternateNumber;
                    existingEmployee.Address = model.Address;
                    existingEmployee.Education = model.Education;
                    existingEmployee.Gender = model.Gender;
                    existingEmployee.IsActive = model.IsActive;
                    existingEmployee.JoiningDate = model.JoiningDate;
                    existingEmployee.EmployeeType = model.EmployeeType;
                    existingEmployee.AdharNo = model.AdharNo;
                    existingEmployee.Shift = model.Shift;
                    existingEmployee.Salary = model.Salary;
                    existingEmployee.ModifiedAt = CurrentDate;
                    existingEmployee.ModifiedBy = 1;
                    _context.EmpEmployees.Update(existingEmployee);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        } 
        #endregion
    }
}
