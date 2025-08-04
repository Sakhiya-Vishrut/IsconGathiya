using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IsconGathiya.Domain.DataModels;

[Table("LOC_States")]
public partial class LocState
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("country_id")]
    public int CountryId { get; set; }

    [Column("country_code")]
    [StringLength(2)]
    public string CountryCode { get; set; } = null!;

    [Column("fips_code")]
    [StringLength(255)]
    public string? FipsCode { get; set; }

    [Column("iso2")]
    [StringLength(255)]
    public string? Iso2 { get; set; }

    [Column("type")]
    [StringLength(191)]
    public string? Type { get; set; }

    [Column("latitude")]
    [Precision(10, 8)]
    public decimal? Latitude { get; set; }

    [Column("longitude")]
    [Precision(11, 8)]
    public decimal? Longitude { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    [Column("flag")]
    public short Flag { get; set; }

    [Column("wikiDataId")]
    [StringLength(255)]
    public string? WikiDataId { get; set; }

    [InverseProperty("State")]
    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    [ForeignKey("CountryId")]
    [InverseProperty("LocStates")]
    public virtual LocCountry Country { get; set; } = null!;

    [InverseProperty("StateNavigation")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    [InverseProperty("State")]
    public virtual ICollection<LocCity> LocCities { get; set; } = new List<LocCity>();
}
