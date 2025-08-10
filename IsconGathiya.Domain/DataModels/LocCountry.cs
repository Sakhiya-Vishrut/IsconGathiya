using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IsconGathiya.Domain.DataModels;

[Table("LOC_Country")]
public partial class LocCountry
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("iso3")]
    [StringLength(3)]
    public string? Iso3 { get; set; }

    [Column("numeric_code")]
    [StringLength(3)]
    public string? NumericCode { get; set; }

    [Column("iso2")]
    [StringLength(2)]
    public string? Iso2 { get; set; }

    [Column("phonecode")]
    [StringLength(255)]
    public string? Phonecode { get; set; }

    [Column("capital")]
    [StringLength(255)]
    public string? Capital { get; set; }

    [Column("currency")]
    [StringLength(255)]
    public string? Currency { get; set; }

    [Column("currency_name")]
    [StringLength(255)]
    public string? CurrencyName { get; set; }

    [Column("currency_symbol")]
    [StringLength(255)]
    public string? CurrencySymbol { get; set; }

    [Column("tld")]
    [StringLength(255)]
    public string? Tld { get; set; }

    [Column("native")]
    [StringLength(255)]
    public string? Native { get; set; }

    [Column("region")]
    [StringLength(255)]
    public string? Region { get; set; }

    [Column("region_id")]
    public long? RegionId { get; set; }

    [Column("subregion")]
    [StringLength(255)]
    public string? Subregion { get; set; }

    [Column("subregion_id")]
    public long? SubregionId { get; set; }

    [Column("nationality")]
    [StringLength(255)]
    public string? Nationality { get; set; }

    [Column("timezones")]
    public string? Timezones { get; set; }

    [Column("translations")]
    public string? Translations { get; set; }

    [Column("latitude")]
    [Precision(10, 8)]
    public decimal? Latitude { get; set; }

    [Column("longitude")]
    [Precision(11, 8)]
    public decimal? Longitude { get; set; }

    [Column("emoji")]
    [StringLength(191)]
    public string? Emoji { get; set; }

    [Column("emojiU")]
    [StringLength(191)]
    public string? EmojiU { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    [Column("flag")]
    public short Flag { get; set; }

    [Column("wikiDataId")]
    [StringLength(255)]
    public string? WikiDataId { get; set; }

    [InverseProperty("Country")]
    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();

    [InverseProperty("Country")]
    public virtual ICollection<LocState> LocStates { get; set; } = new List<LocState>();
}
