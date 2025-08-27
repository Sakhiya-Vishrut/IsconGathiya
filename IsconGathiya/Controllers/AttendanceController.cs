using IsconGathiya.Common;
using IsconGathiya.Common.Utility;
using IsconGathiya.Helper;
using IsconGathiya.Helper.Mapper.Attendance;
using IsconGathiya.Service.Attendance;
using IsconGathiya.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace IsconGathiya.Controllers
{
    [AuthManager]
    public class AttendanceController : BaseController
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceController(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public IActionResult Index(short? shiftType = null)
        {
            try
            {
                var employee = new EmployeeViewModel();
                employee.ShiftTypeList = EnumHelper.GetEnumSelectList<Enums.Shift>();


                var selectedShift = shiftType ?? (short)Enums.Shift.Day;

                employee.employeeDetailsList = _attendanceRepository
                    .GetEmployeeDataWithFilter(int.Parse(CV.Branch()), selectedShift)
                    .ToModel();
                employee.employeeDetails.BranchList = _attendanceRepository.GetBranchList();

                return View(employee);
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while fetching employee data.", e);
            }
        }

        #region Index

        public IActionResult ChangeBranch(short? shiftType = null)
        {
            try
            {
                var employee = new EmployeeViewModel();
                employee.ShiftTypeList = EnumHelper.GetEnumSelectList<Enums.Shift>();
                

                var selectedShift = shiftType ?? (short)Enums.Shift.Day;

                employee.employeeDetailsList = _attendanceRepository
                    .GetEmployeeDataWithFilter(int.Parse(CV.Branch()), selectedShift)
                    .ToModel();
                employee.employeeDetails.BranchList = _attendanceRepository.GetBranchList();

                return View(employee);
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while fetching employee data.", e);
            }
        }

        #endregion

        [HttpGet]
        public IActionResult GetEmployeesByShift(short shiftType)
        {
            var employeeVM = new EmployeeViewModel
            {
                employeeDetailsList = _attendanceRepository
                    .GetEmployeeDataWithFilter(int.Parse(CV.Branch()), shiftType)
                    .ToModel()

            };
            employeeVM.employeeDetails.BranchList = _attendanceRepository.GetBranchList();


            return PartialView("_ChangeBranchGridBody", employeeVM);
        }

        [HttpGet]
        public IActionResult GetAttendanceEmployeesByShift(short shiftType)
        {
            var employeeVM = new EmployeeViewModel
            {
                employeeDetailsList = _attendanceRepository
                    .GetEmployeeDataWithFilter(int.Parse(CV.Branch()), shiftType)
                    .ToModel()

            };
            employeeVM.employeeDetails.BranchList = _attendanceRepository.GetBranchList();


            return PartialView("_Partial_Attendance_GridBody", employeeVM);
        }

        [HttpPost]
        public IActionResult ChangeBranch(EmployeeViewModel model, List<int> SelectedEmployeeIds)
        {
            try
            {
                if (model.employeeDetails.BranchId.HasValue && SelectedEmployeeIds.Any())
                {
                    int newBranchId = model.employeeDetails.BranchId.Value;

                    foreach (var employeeId in SelectedEmployeeIds)
                    {
                        _attendanceRepository.UpdateEmployeeBranch(employeeId, newBranchId);
                    }

                    AddSweetAlertSuccessPopup(ConstantMessage.EmployeeChange);
                    return RedirectToAction("ChangeBranch");
                }
                else
                {
                    model.employeeDetails.BranchList = _attendanceRepository.GetBranchList();
                    model.employeeDetailsList = _attendanceRepository.GetEmployeeDataWithFilter(int.Parse(CV.Branch()), (short)Enums.Shift.Day).ToModel(); // ડિફોલ્ટ શિફ્ટ
                    return View(model);
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("ChangeBranch");
            }
        }
    }
}
