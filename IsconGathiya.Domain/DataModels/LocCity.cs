using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IsconGathiya.Domain.DataModels;

[Table("LOC_Cities")]
public partial class LocCity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("state_id")]
    public int StateId { get; set; }

    [Column("state_code")]
    [StringLength(255)]
    public string StateCode { get; set; } = null!;

    [Column("country_id")]
    public int CountryId { get; set; }

    [Column("country_code")]
    [StringLength(2)]
    public string CountryCode { get; set; } = null!;

    [Column("latitude")]
    [Precision(10, 8)]
    public decimal Latitude { get; set; }

    [Column("longitude")]
    [Precision(11, 8)]
    public decimal Longitude { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    [Column("flag")]
    public short Flag { get; set; }

    [Column("wikiDataId")]
    [StringLength(255)]
    public string? WikiDataId { get; set; }

    [InverseProperty("City")]
    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    [InverseProperty("City")]
    public virtual ICollection<EmpEmployee> EmpEmployees { get; set; } = new List<EmpEmployee>();

    [ForeignKey("StateId")]
    [InverseProperty("LocCities")]
    public virtual LocState State { get; set; } = null!;
}
