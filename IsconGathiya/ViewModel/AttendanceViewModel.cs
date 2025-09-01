using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IsconGathiya.ViewModel
{
    public class AttendanceViewModel : BaseModelViewModel
    {
        public AttendanceViewModel()
        {
            attendanceDetailsList = new List<AttendanceDetails>();
            attendanceDetails = new AttendanceDetails();
        }

        public List<AttendanceDetails> attendanceDetailsList { get; set; }
        public AttendanceDetails attendanceDetails { get; set; }


        public class AttendanceDetails
        {
            public int AttendancId { get; set; }

            public int EmployeeId { get; set; }

            public DateTime? SignInDate { get; set; }

            public DateTime? SignoutDate { get; set; }

            public short? Status { get; set; }

            public string? Reason { get; set; }

            public DateTime? CreatedAt { get; set; }

            public DateTime? ModifiedAt { get; set; }
        }
    }
}
