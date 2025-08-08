using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IsconGathiya.Domain.DataModels;

[Table("EMP_Employee")]
[Index("AdharNo", Name = "Employee_AdharNo_key", IsUnique = true)]
[Index("Email", Name = "Employee_Email_key", IsUnique = true)]
[Index("StaffId", Name = "Employee_StaffId_key", IsUnique = true)]
public partial class EmpEmployee
{
    [Key]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [Column("BranchID")]
    public int? BranchId { get; set; }

    public int? CityId { get; set; }

    public int? StateId { get; set; }

    [StringLength(100)]
    public string EmployeeName { get; set; } = null!;

    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string? MiddleName { get; set; }

    [StringLength(500)]
    public string StaffId { get; set; } = null!;

    [StringLength(150)]
    public string MobileNumber { get; set; } = null!;

    [StringLength(150)]
    public string? WhatsAppNumber { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? BirthDate { get; set; }

    [StringLength(150)]
    public string? Email { get; set; }

    [StringLength(150)]
    public string? AlternateNumber { get; set; }

    public string? Address { get; set; }

    [StringLength(500)]
    public string? EmployeeImage { get; set; }

    [StringLength(200)]
    public string? Education { get; set; }

    public short? Gender { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? JoiningDate { get; set; }

    public short? EmployeeType { get; set; }

    [StringLength(500)]
    public string? AdharCardImage { get; set; }

    [StringLength(100)]
    public string? AdharNo { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    public int? DeletedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ModifiedAt { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    public int? CountryId { get; set; }

    public bool? Shift { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("EmpEmployees")]
    public virtual Branch? Branch { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("EmpEmployees")]
    public virtual LocCity? City { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("EmpEmployees")]
    public virtual LocCountry? Country { get; set; }

    [ForeignKey("StateId")]
    [InverseProperty("EmpEmployees")]
    public virtual LocState? State { get; set; }
}
