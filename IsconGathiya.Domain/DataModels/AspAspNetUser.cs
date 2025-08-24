using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IsconGathiya.Domain.DataModels;

[Table("ASP_AspNetUser")]
public partial class AspAspNetUser
{
    [Key]
    [Column("AspNetUserID")]
    public Guid AspNetUserId { get; set; }

    [StringLength(200)]
    public string UserName { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    [StringLength(100)]
    public string? PhoneNumber { get; set; }

    public bool? IsDeleted { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Created { get; set; }

    public string? Token { get; set; }

    [InverseProperty("AspNetUser")]
    public virtual ICollection<SecAdmin> SecAdmins { get; set; } = new List<SecAdmin>();
}
