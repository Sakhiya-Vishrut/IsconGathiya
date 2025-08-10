using IsconGathiya.Common;
using IsconGathiya.Common.Utility;
using IsconGathiya.Helper;
using IsconGathiya.Helper.Mapper;
using IsconGathiya.Helper.Mapper.Employee;
using IsconGathiya.Service.Employee;
using IsconGathiya.Service.Location;
using IsconGathiya.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace IsconGathiya.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILocationRepository _locationrepository;

        public EmployeeController(IEmployeeRepository employeeRepository, ILocationRepository locationRepository)
        {
            _employeeRepository = employeeRepository;
            _locationrepository = locationRepository;
        }
        public IActionResult Index()
        {
            try
            {
                var employee = new EmployeeViewModel();
                employee.PageTitle = "employee List";
                employee.BreadcrumbTitle = "employee Management";
                employee.BreadcrumbParent = "employee";
                employee.BreadcrumbChild = "employee List";
                employee.PageSize = ConfigItems.DefaultPageSize;
                employee.PageIndex = ConfigItems.DefaultPageNumber;
                employee.SortDirection = "desc";
                employee.ColumnName = "ModifiedAt";
                employee.EmployeeTypeList = EnumHelper.GetEnumSelectList<Enums.EmployeeType>();
                employee.GenderTypeList = EnumHelper.GetEnumSelectList<Enums.Gender>();
                employee.ShiftTypeList = EnumHelper.GetEnumSelectList<Enums.Shift>();
                employee.employeeDetailsList = _employeeRepository.GetEmployeeDataWithFilter(null, null, employee.PageSize, employee.PageIndex, employee.ColumnName, employee.SortDirection).ToModel();
                return View(employee);
            }
            catch(Exception e)
            {
                throw;
            }
        }

        [HttpGet, Route("Employee/search", Name = "Employee_Filter")]
        public IActionResult EmployeeFilterList(int pageIndex, int pageSize, string filterObj, string columnName, string sortDirection)
        {
            try
            {

                var Viewmodel = new EmployeeViewModel();
                Viewmodel.PageSize = pageSize;
                Viewmodel.PageSize = pageSize;
                Viewmodel.PageIndex = pageIndex;
                Viewmodel.BreadcrumbTitle = "Employee Management";
                Viewmodel.BreadcrumbParent = "Employee";
                Viewmodel.BreadcrumbChild = "Employee List";
                Viewmodel.ColumnName = columnName;

                int? filterState = null;
                int? filterCity = null;

                if (!string.IsNullOrEmpty(filterObj))
                {
                    filterState = CommonHelper.GetFilterPropertyValueInt(filterObj, "stateList");
                    filterCity = CommonHelper.GetFilterPropertyValueInt(filterObj, "cityList");
                }

                Viewmodel.employeeDetailsList = _employeeRepository.GetEmployeeDataWithFilter(filterState, filterCity, Viewmodel.PageSize, Viewmodel.PageIndex, columnName, sortDirection).ToModel();

                return PartialView("_Partial_Employee_GridBody", Viewmodel);
            }
            catch (Exception ex)
            {
                return RedirectToRoute("Error_404");
            }
        }

        [HttpGet, Route("Employee/EmployeeForm", Name = "Employee_Add")]
        [HttpGet, Route("Employee/EmployeeForm/{encodeEmployeeId}", Name = "Employee_Edit")]
        public IActionResult EmployeeForm(string encodeEmployeeId)
        {
            try
            {
                var model = new EmployeeViewModel();
                model.BreadcrumbTitle = "Problem Management";
                model.BreadcrumbParent = "Problem";
                model.BreadcrumbChild = string.IsNullOrEmpty(encodeEmployeeId) ? "Add Employee" : "Update Employee";
                model.PageTitle = string.IsNullOrEmpty(encodeEmployeeId) ? "Add Employee" : "Upadte Employee";
                if (encodeEmployeeId != null)
                {
                    int? ProblemId = encodeEmployeeId.Decode();
                    model.employeeDetails = _employeeRepository.GetEmployeeDetails(ProblemId).ToModel();
                }
                model.employeeDetails.BranchList = _employeeRepository.GetBranchList();
                model.employeeDetails.CountryList = _locationrepository.GetCountryList();
                model.EmployeeTypeList = EnumHelper.GetEnumSelectList<Enums.EmployeeType>();
                model.GenderTypeList = EnumHelper.GetEnumSelectList<Enums.Gender>();
                model.ShiftTypeList = EnumHelper.GetEnumSelectList<Enums.Shift>();
                return View(model);
            }
            catch (Exception ex)
            {
                //AddSweetAlertErrorPopup(ex.Message);
                //return RedirectToAction("Index", "Problem");
                return RedirectToRoute("Error_404");
            }
        }
        #region SaveProblem
        [HttpPost, Route("Employee/save", Name = "Employee_Save")]
        public async Task<IActionResult> SaveEmployee(EmployeeViewModel employeeViewModel)
        {
            try
            {
                var isSuccess = await _employeeRepository.AddEditEmployee(employeeViewModel.employeeDetails.ToModel());
                string successMessage = string.Empty;
                if (isSuccess)
                {
                    successMessage = (employeeViewModel.employeeDetails.EmployeeId == null || employeeViewModel.employeeDetails.EmployeeId == 0)
                        ? ConstantMessage.EmployeeAdded
                        : ConstantMessage.EmployeeEdit;
                    AddSweetAlertSuccessPopup(successMessage);
                }
                else
                {
                    AddSweetAlertErrorPopup("Somthing went wrong");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToRoute("Error_404");
            }
        }
        #endregion

        #region DeleteProblem
        [HttpPost, Route("Employee/delete", Name = "Employee_Delete")]

        public async Task<IActionResult> EmployeeDelete(string? encodeEmployeeId)
        {
            try
            {
                if (!string.IsNullOrEmpty(encodeEmployeeId))
                {
                    int? EmployeeId = encodeEmployeeId.Decode();
                    var isDeleteSuccess = await _employeeRepository.DeleteEmployee(EmployeeId);
                    if (!isDeleteSuccess)
                    {
                        AddSweetAlertErrorPopup(ConstantMessage.EmployeeDeleteSuccess);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Json(new { success = true });
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToRoute("Error_404");
            }
        }
        #endregion
    }
}
