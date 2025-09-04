using IsconGathiya.Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsconGathiya.Domain
{
    public class TotalRecord
    {
        public int TotalRecords { get; set; }
        public LocState LocState { get; set; }
        public LocCity LocCity { get; set; }
        public LocCountry LocCountry { get; set; }
    }

    public class BranchDTO : TotalRecord
    {
        public Branch branch { get; set; }
    }
    // File: DTOs/EmployeeDTO.cs
    public class EmployeeDTO : TotalRecord
    {
        public EmpEmployee employee { get; set; }
        public LocCity city { get; set; }
        public LocState state { get; set; }
        public Branch branch { get; set; }
        public EmpAttendance attendance { get; set; }
    }
    public class AttandenceDTO : TotalRecord
    {
        public EmpEmployee employee { get; set; }
        public EmpAttendance EmpAttendanc { get; set; }
    }
}
