using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Common
{
    public static class ExceptionMessages
    {
        public static string EmailExist => "Email already exists!";
        public static string PhoneNumberlExist => "Cell number already exists!";
        public static string ValidationSelectUserType => "Please select user type !";
        public static string CommonExceptionMessage => "Something went wrong, please try again";
        public static string PhoneNumberOrEmailNotFoundForSelectedCard => "Email or cell number no not found for selected card.";
        public static string DoNotHaveAccess => "You do not have access to this page";
    }
}
