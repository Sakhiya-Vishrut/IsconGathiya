using IsconGathiya.Common;
using IsconGathiya.Common.Utility;
using IsconGathiya.Domain.DataModels;
using IsconGathiya.Helper;
using IsconGathiya.Helper.Mapper;
using IsconGathiya.Helper.Mapper.Attendance;
using IsconGathiya.Service.Attendance;
using IsconGathiya.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;

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

        private DateTime GetAttendanceDate()
        {
            DateTime currentDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            bool isBetweenMidnightAnd7AM = currentDate.TimeOfDay >= TimeSpan.Zero && currentDate.TimeOfDay < TimeSpan.FromHours(7);
            return (isBetweenMidnightAnd7AM ? currentDate.AddDays(-1) : currentDate).Date;
        }

        #region Index
        [HttpGet, Route("Attendance/Index", Name = "Index")]
        public IActionResult Index(short? shiftType = null)
        {
            try
            {
                var employee = new EmployeeViewModel();
                employee.ShiftTypeList = EnumHelper.GetEnumSelectList<Enums.Shift>();

                DateTime currentDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                TimeSpan nowTime = currentDate.TimeOfDay;
                bool isDayTime = nowTime >= TimeSpan.FromHours(7) && nowTime < TimeSpan.FromHours(19);
                bool isNightShift = nowTime >= TimeSpan.FromHours(19) || nowTime < TimeSpan.FromHours(7);
                var selectedShift = shiftType ?? (isDayTime ? (short)Enums.Shift.Day : (short)Enums.Shift.Night);

                var attendanceDate = (isNightShift && nowTime < TimeSpan.FromHours(7)) ? currentDate.AddDays(-1).Date : currentDate.Date;
                employee.employeeDetailsList = _attendanceRepository.GetEmployeeDataWithFilter(int.Parse(CV.Branch()), selectedShift, attendanceDate).ToModel();
                employee.employeeDetails.BranchList = _attendanceRepository.GetBranchList();
                employee.IsAttendanceCompleted = _attendanceRepository.IsAttendanceCompleted(int.Parse(CV.Branch()), attendanceDate, isNightShift);

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
                var attendanceDate = GetAttendanceDate();
                employee.employeeDetailsList = _attendanceRepository.GetEmployeeDataWithFilter(int.Parse(CV.Branch()), selectedShift, attendanceDate).ToModel();
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
            var attendanceDate = GetAttendanceDate();
            var employeeVM = new EmployeeViewModel
            {
                employeeDetailsList = _attendanceRepository.GetEmployeeDataWithFilter(int.Parse(CV.Branch()), shiftType, attendanceDate).ToModel()
            };
            employeeVM.employeeDetails.BranchList = _attendanceRepository.GetBranchList();
            employeeVM.ShiftTypeList = EnumHelper.GetEnumSelectList<Enums.Shift>();

            return PartialView("_ChangeBranchGridBody", employeeVM);
        }
        #endregion

        #region GetEmployeesAttendenceByShift
        [HttpGet]
        public IActionResult GetEmployeesAttendenceByShift(short shiftType)
        {
            var attendanceDate = GetAttendanceDate();
            var employeeVM = new EmployeeViewModel
            {
                employeeDetailsList = _attendanceRepository.GetEmployeeDataWithFilter(int.Parse(CV.Branch()), shiftType, attendanceDate).ToModel()
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
            var attendanceDate = GetAttendanceDate();
            var employeeVM = new EmployeeViewModel
            {
                employeeDetailsList = _attendanceRepository.GetEmployeeDataWithFilter(int.Parse(CV.Branch()), shiftType, attendanceDate).ToModel()
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
                        _attendanceRepository.UpdateEmployeeBranch(employeeId, newBranchId, shift);
                    }

                    AddSweetAlertSuccessPopup(ConstantMessage.EmployeeChange);
                    return RedirectToAction("ChangeBranch");
                }
                else
                {
                    model.employeeDetails.BranchList = _attendanceRepository.GetBranchList();
                    var attendanceDate = GetAttendanceDate();
                    model.employeeDetailsList = _attendanceRepository.GetEmployeeDataWithFilter(int.Parse(CV.Branch()), (short)Enums.Shift.Day, attendanceDate).ToModel();
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
                var branchId = int.Parse(CV.Branch());
                DateTime currentDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                TimeSpan nowTime = currentDate.TimeOfDay;
                bool isBetweenMidnightAnd7AM = nowTime >= TimeSpan.Zero && nowTime < TimeSpan.FromHours(7);
                bool isNightShift = nowTime >= TimeSpan.FromHours(19) || isBetweenMidnightAnd7AM;

                bool isValidShift = (isNightShift && (nowTime >= TimeSpan.FromHours(19) || nowTime < TimeSpan.FromHours(7))) ||
                                   (!isNightShift && nowTime >= TimeSpan.FromHours(7) && nowTime < TimeSpan.FromHours(19));
                if (!isValidShift)
                {
                    AddSweetAlertWarningPopup("Invalid shift selection for the current time.");
                    return RedirectToAction("Index");
                }

                DateTime attendanceDate = isNightShift && isBetweenMidnightAnd7AM ? currentDate.AddDays(-1).Date : currentDate.Date;

                bool isAttendanceCompleted = _attendanceRepository.IsAttendanceCompleted(branchId, attendanceDate, isNightShift);
                if (isAttendanceCompleted)
                {
                    AddSweetAlertWarningPopup(ConstantMessage.ShiftAttendanceComplate);
                    return RedirectToAction("Index");
                }

                bool isSuccess = await _attendanceRepository.AddEditAttendance(model.attendanceDetailsList.ToModel(), isBetweenMidnightAnd7AM);

                if (isSuccess)
                {
                    AddSweetAlertSuccessPopup(ConstantMessage.AttendanceComplate);
                }
                else
                {
                    AddSweetAlertErrorPopup("Failed to save attendance. Please try again.");
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