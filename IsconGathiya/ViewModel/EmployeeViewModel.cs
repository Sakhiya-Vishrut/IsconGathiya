// File: ViewModels/EmployeeViewModel.cs
using IsconGathiya.Domain.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static IsconGathiya.ViewModel.EmployeeViewModel;

namespace IsconGathiya.ViewModel
{
    public class EmployeeViewModel : BaseModelViewModel
    {
        public EmployeeViewModel()
        {
            employeeDetailsList = new List<EmployeeDetails>();
            employeeDetails = new EmployeeDetails();
        }

        public List<EmployeeDetails> employeeDetailsList { get; set; }
        public List<SelectListItem> EmployeeTypeList { get; set; }
        public List<SelectListItem> GenderTypeList { get; set; }
        public List<SelectListItem> ShiftTypeList { get; set; }
        public EmployeeDetails employeeDetails { get; set; }
        public bool IsAttendanceCompleted { get; set; }
        public bool CanSaveAttendance { get; set; }
        public class EmployeeDetails
        {
            public int EmployeeId { get; set; }
            [Required(ErrorMessage = "Branch is required.")]

            public int? BranchId { get; set; }
            [Required(ErrorMessage = "City is required.")]
            public int? CityId { get; set; }
            [Required(ErrorMessage = "State is required.")]
            public int? StateId { get; set; }
            [Required(ErrorMessage = "Country is required.")]
            public int? CountryId { get; set; }

            [Required(ErrorMessage = "First Name is required.")]
            [StringLength(100, ErrorMessage = "First Name cannot exceed 100 characters.")]
            public string EmployeeName { get; set; } = null!;

            [Required(ErrorMessage = "Staff ID is required.")]
            [RegularExpression(@"^\d{3}$", ErrorMessage = "StafID must be exactly 3 digits.")]
            public string StaffId { get; set; } = null!;

            [StringLength(100, ErrorMessage = "State cannot exceed 100 characters.")]
            public string? State { get; set; }

            [StringLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
            public string? City { get; set; }

            [Required(ErrorMessage = "Mobile Number is required.")]
            [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile Number must be exactly 10 digits.")]
            public string MobileNumber { get; set; } = null!;

            [RegularExpression(@"^\d{10}$", ErrorMessage = "WhatsApp Number must be exactly 10 digits.")]
            public string? WhatsAppNumber { get; set; }

            [Required(ErrorMessage = "Birth Date is required.")]
            public DateTime? BirthDate { get; set; }

            [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email format is invalid.")]
            public string? Email { get; set; }

            [RegularExpression(@"^\d{10}$", ErrorMessage = "Alternate Number must be exactly 10 digits.")]
            public string? AlternateNumber { get; set; }

            [Required(ErrorMessage = "Address is required.")]
            [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters.")]
            public string? Address { get; set; }

            [Required(ErrorMessage = "Family Member Name is required.")]
            public string? FamilyMemberName { get; set; }

            [Required(ErrorMessage = "Mobile Number is required.")]
            [RegularExpression(@"^\d{10}$", ErrorMessage = "Alternate Number must be exactly 10 digits.")]
            public string? FamilyNumber { get; set; }

            public string? EmployeeImage { get; set; }

            [StringLength(100, ErrorMessage = "Education cannot exceed 100 characters.")]
            [Required(ErrorMessage = "Education is required.")]
            public string? Education { get; set; }

            [Required(ErrorMessage = "Gender is required.")]
            public short? Gender { get; set; }

            [Required(ErrorMessage = "Is Active status is required.")]
            public bool? IsActive { get; set; } = false;

            [Required(ErrorMessage = "Joining Date is required.")]
            public DateTime? JoiningDate { get; set; }

            [Required(ErrorMessage = "Employee Type is required.")]
            public short? EmployeeType { get; set; }

            [StringLength(500, ErrorMessage = "Aadhar Card Image cannot exceed 500 characters.")]
            public string? AdharCardImage { get; set; }

            [Required(ErrorMessage = "Aadhar Number is required.")]
            [RegularExpression(@"^\d{12}$", ErrorMessage = "Aadhar Number must be a 12 digit number.")]
            public string? AdharNo { get; set; }
            [Required(ErrorMessage = "Shift is required.")]
            public short? Shift { get; set; }
            public double? Salary { get; set; }
            public string CityName { get; set; }
            public string StateName { get; set; }
            public string CountryName { get; set; }
            public string BranchName { get; set; }
            public string ShiftType { get; set; }
            public string? EmployeeTypeName { get; set; }
            public string? GenderType { get; set; }
            public List<LocCountry> CountryList { get; set; }
            public List<LocCity> CityList { get; set; }
            public List<LocState> StateList { get; set; }
            public List<Branch> BranchList { get; set; }
            public int TotalRecords { get; set; }
            public List<int> SelectedEmployeeIds { get; set; }
            public int? AttendanceId { get; set; }
            public short? AttendanceStatus { get; set; }
        }
    }
}