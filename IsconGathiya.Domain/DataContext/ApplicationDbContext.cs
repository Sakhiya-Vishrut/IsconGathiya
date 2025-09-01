using System;
using System.Collections.Generic;
using IsconGathiya.Domain.DataModels;
using Microsoft.EntityFrameworkCore;

namespace IsconGathiya.Domain.DataContext;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspAspNetUser> AspAspNetUsers { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<EmpAttendance> EmpAttendances { get; set; }

    public virtual DbSet<EmpEmployee> EmpEmployees { get; set; }

    public virtual DbSet<LocCity> LocCities { get; set; }

    public virtual DbSet<LocCountry> LocCountries { get; set; }

    public virtual DbSet<LocState> LocStates { get; set; }

    public virtual DbSet<SecAdmin> SecAdmins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID=postgres;Password=123;Server=localhost;Port=5432;Database=IsconGathiya;Pooling=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspAspNetUser>(entity =>
        {
            entity.HasKey(e => e.AspNetUserId).HasName("ASP_AspNetUser_pkey");

            entity.Property(e => e.AspNetUserId).ValueGeneratedNever();
            entity.Property(e => e.Created).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("Branch_pkey");

            entity.HasOne(d => d.City).WithMany(p => p.Branches).HasConstraintName("Branch_CityId_fkey");

            entity.HasOne(d => d.Country).WithMany(p => p.Branches).HasConstraintName("Branch_Country_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.Branches).HasConstraintName("Branch_StateId_fkey");
        });

        modelBuilder.Entity<EmpAttendance>(entity =>
        {
            entity.HasKey(e => e.AttendancId).HasName("EMP_Attendanc_pkey");

            entity.Property(e => e.AttendancId).HasDefaultValueSql("nextval('\"EMP_Attendanc_AttendancId_seq\"'::regclass)");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmpAttendances)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EMP_Attendanc_EmployeeId_fkey");
        });

        modelBuilder.Entity<EmpEmployee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("Employee_pkey");

            entity.Property(e => e.EmployeeId).HasDefaultValueSql("nextval('\"Employee_EmployeeID_seq\"'::regclass)");
            entity.Property(e => e.IsActive).HasDefaultValue(false);

            entity.HasOne(d => d.Branch).WithMany(p => p.EmpEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employee_BranchID_fkey");

            entity.HasOne(d => d.City).WithMany(p => p.EmpEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employee_CityId_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.EmpEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Employee_StateId_fkey");
        });

        modelBuilder.Entity<LocCity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("loc_cities_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CountryCode).IsFixedLength();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("'2014-01-01 12:01:01'::timestamp without time zone");
            entity.Property(e => e.Flag).HasDefaultValue((short)1);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.State).WithMany(p => p.LocCities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stateid");
        });

        modelBuilder.Entity<LocCountry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("loc_countries_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Flag).HasDefaultValue((short)1);
            entity.Property(e => e.Iso2).IsFixedLength();
            entity.Property(e => e.Iso3).IsFixedLength();
            entity.Property(e => e.NumericCode).IsFixedLength();
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<LocState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("loc_states_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CountryCode).IsFixedLength();
            entity.Property(e => e.Flag).HasDefaultValue((short)1);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Country).WithMany(p => p.LocStates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_countryid");
        });

        modelBuilder.Entity<SecAdmin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("SEC_Admin_pkey");

            entity.HasOne(d => d.AspNetUser).WithMany(p => p.SecAdmins).HasConstraintName("AspNetUserID");

            entity.HasOne(d => d.Branch).WithMany(p => p.SecAdmins).HasConstraintName("SEC_Admin_BranchId_fkey");
        });
        modelBuilder.HasSequence("SEC_Admin_AdminID_seq").HasMax(2147483647L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
