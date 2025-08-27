using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace IsconGathiya.Common
{
    public class ConstantMessage
    {

        //Branch
        public const string Branch = "Branch has been added successfully.";
        public const string BranchEditSuccessful = "Branch details have been updated successfully.";
        public const string BranchAddOrEditUnsuccessful = "Failed to add or update the Branch.";
        public const string BranchDeleteSuccessful = "Branch has been deleted successfully.";
        public const string BranchDeleteUnsuccessful = "Failed to delete the Branch.";
        public const string BranchNameAlreadyExists = "A Branch with this name already exists.";
        public const string BranchNotExists = "The specified Branch does not exist.";

        //Employee
        public const string EmployeeAdded = "Employee is Added Successfully.";
        public const string EmployeeEdit = "Employee is Edited Successfully.";
        public const string EmployeeDeleteUnSuccess = "Employee Deleted UnSuccessfully.";
        public const string EmployeeAlreadyExist = "Employee is allready exists.";
        public const string EmployeeNotFound = "Employee not found.";
        public const string EmployeeDeleteSuccess = "Employee deleted successfully.";

        // Attendance

        public const string EmployeeChange = "Employees is Change Successfully.";

    }
}
