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

        public IActionResult Index()
        {
            return View();
        }

        #region Index

        public IActionResult ChangeBranch(short? shiftType = null)
        {
            try
            {
                var employee = new EmployeeViewModel();
                employee.ShiftTypeList = EnumHelper.GetEnumSelectList<Enums.Shift>();
                employee.employeeDetails.BranchList = _attendanceRepository.GetBranchList();

                var selectedShift = shiftType ?? (short)Enums.Shift.Day;

                employee.employeeDetailsList = _attendanceRepository
                    .GetEmployeeDataWithFilter(int.Parse(CV.Branch()), selectedShift)
                    .ToModel();

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

            return PartialView("_ChangeBranchGridBody", employeeVM);
        }
    }
}
