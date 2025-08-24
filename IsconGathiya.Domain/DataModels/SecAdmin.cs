using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IsconGathiya.Domain.DataModels;

[Table("SEC_Admin")]
public partial class SecAdmin
{
    [Key]
    [Column("AdminID")]
    public int AdminId { get; set; }

    [StringLength(250)]
    public string? UserName { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Created { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Modified { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Deleted { get; set; }

    public Guid? AspNetUserId { get; set; }

    [StringLength(250)]
    public string? Email { get; set; }

    public short? RollType { get; set; }

    [ForeignKey("AspNetUserId")]
    [InverseProperty("SecAdmins")]
    public virtual AspAspNetUser? AspNetUser { get; set; }
}
