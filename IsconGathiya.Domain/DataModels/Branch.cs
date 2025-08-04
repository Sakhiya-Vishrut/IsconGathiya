using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IsconGathiya.Domain.DataModels;

[Table("Branch")]
public partial class Branch
{
    [Key]
    [Column("BranchID")]
    public int BranchId { get; set; }

    public int? CityId { get; set; }

    public int? StateId { get; set; }

    [StringLength(100)]
    public string BranchName { get; set; } = null!;

    public string? Address { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    public int? ModifiedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? ModifiedAt { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("Branches")]
    public virtual LocCity? City { get; set; }

    [InverseProperty("Branch")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [ForeignKey("StateId")]
    [InverseProperty("Branches")]
    public virtual LocState? State { get; set; }
}
