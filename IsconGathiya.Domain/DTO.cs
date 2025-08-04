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
    }

    public class BranchDTO : TotalRecord
    {
        public Branch branch { get; set; }
        public List<LocCity> CityNameList { get; set; }
        public List<LocState> StateNameList { get; set; }
    }
}
