using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IsconGathiya.Domain.DataModels;

[Table("EMP_Attendance")]
public partial class EmpAttendance
{
    [Key]
    public int AttendancId { get; set; }

    public int EmployeeId { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? SignInDate { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? SignoutDate { get; set; }

    public short? Status { get; set; }

    public string? Reason { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ModifiedAt { get; set; }

    public Guid ModifiedBy { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmpAttendances")]
    public virtual EmpEmployee Employee { get; set; } = null!;
}
