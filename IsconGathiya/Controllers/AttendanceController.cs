using IsconGathiya.Common;
using IsconGathiya.Common.Utility;
using IsconGathiya.Domain.DataModels;
using IsconGathiya.Helper;
using IsconGathiya.Helper.Mapper;
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

        #region Index
        [HttpGet, Route("Attendance/Index", Name = "Index")]
        public IActionResult Index(short? shiftType = null)
        {
            try
            {
                var employee = new EmployeeViewModel();
                employee.ShiftTypeList = EnumHelper.GetEnumSelectList<Enums.Shift>();

                var selectedShift = shiftType ?? (short)Enums.Shift.Day;
                employee.employeeDetailsList = _attendanceRepository.GetEmployeeDataWithFilter(int.Parse(CV.Branch()), selectedShift).ToModel();
                employee.employeeDetails.BranchList = _attendanceRepository.GetBranchList();

                return View(employee);
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while fetching employee data.", e);
            }
        }
        #endregion

        #region ChangeBranch
        [HttpGet, Route("Attendance/ChangeBranch", Name = "ChangeBranch")]
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

        #region GetEmployeesByShift
        [HttpGet]
        public IActionResult GetEmployeesByShift(short shiftType)
        {
            var employeeVM = new EmployeeViewModel
            {
                employeeDetailsList = _attendanceRepository.GetEmployeeDataWithFilter(int.Parse(CV.Branch()), shiftType).ToModel()
            };
            employeeVM.employeeDetails.BranchList = _attendanceRepository.GetBranchList();
            employeeVM.ShiftTypeList = EnumHelper.GetEnumSelectList<Enums.Shift>();

            return PartialView("_ChangeBranchGridBody", employeeVM);
        }
        #endregion

        #region GetEmployeesByShift
        [HttpGet]
        public IActionResult GetEmployeesAttendenceByShift(short shiftType)
        {
            var employeeVM = new EmployeeViewModel
            {
                employeeDetailsList = _attendanceRepository.GetEmployeeDataWithFilter(int.Parse(CV.Branch()), shiftType).ToModel()
            };
            employeeVM.employeeDetails.BranchList = _attendanceRepository.GetBranchList();
            employeeVM.ShiftTypeList = EnumHelper.GetEnumSelectList<Enums.Shift>();

            return PartialView("_Partial_Attendance_GridBody", employeeVM);
        }
        #endregion

        #region GetAttendanceEmployeesByShift
        [HttpGet]
        public IActionResult GetAttendanceEmployeesByShift(short shiftType)
        {
            var employeeVM = new EmployeeViewModel
            {
                employeeDetailsList = _attendanceRepository.GetEmployeeDataWithFilter(int.Parse(CV.Branch()), shiftType).ToModel()
            };
            employeeVM.employeeDetails.BranchList = _attendanceRepository.GetBranchList();

            return PartialView("_Partial_Attendance_GridBody", employeeVM);
        }
        #endregion

        #region ChangeBranchAndShift
        [HttpPost]
        public IActionResult ChangeBranch(EmployeeViewModel model, List<int> SelectedEmployeeIds)
        {
            try
            {
                if (model.employeeDetails.BranchId.HasValue && SelectedEmployeeIds.Any())
                {
                    int newBranchId = model.employeeDetails.BranchId.Value;
                    int shift = model.employeeDetails.Shift ?? (short)Enums.Shift.Day;

                    foreach (var employeeId in SelectedEmployeeIds)
                    {
                        _attendanceRepository.UpdateEmployeeBranch(employeeId, newBranchId,shift);
                    }

                    AddSweetAlertWarinigPopup(ConstantMessage.EmployeeChange);
                    return RedirectToAction("ChangeBranch");
                }
                else
                {
                    model.employeeDetails.BranchList = _attendanceRepository.GetBranchList();
                    model.employeeDetailsList = _attendanceRepository.GetEmployeeDataWithFilter(int.Parse(CV.Branch()), (short)Enums.Shift.Day).ToModel();
                    return View(model);
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("ChangeBranch");
            }

        }
        #endregion

        #region SaveEmployee
        [HttpPost, Route("Attendance/save", Name = "Attendance_Save")]
        public async Task<IActionResult> SaveAttendance(AttendanceViewModel model)
        {
            try
            {
                var adminId = Convert.ToInt32(CV.AdminId());
                DateTime fixedTime = new DateTime(2025, 8, 31, 20, 0, 0, 0);
                TimeSpan nowTime = fixedTime.TimeOfDay;/* DateTime.Now.TimeOfDay;*/
                bool isBetweenMidnightAnd7AM = nowTime >= TimeSpan.Zero && nowTime < TimeSpan.FromHours(7);

                bool isSuccess = await _attendanceRepository.AddEditAttendance(model.attendanceDetailsList.ToModel(),isBetweenMidnightAnd7AM);

                if (isSuccess)
                {
                    AddSweetAlertWarinigPopup(ConstantMessage.Branch);
                }
                else
                {
                    AddSweetAlertWarinigPopup(ConstantMessage.Branch);
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToRoute("Error_404");
            }
        }

        #endregion
    }
}
