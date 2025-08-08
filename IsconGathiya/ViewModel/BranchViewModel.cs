using IsconGathiya.Domain.DataModels;
using System.ComponentModel.DataAnnotations;

namespace IsconGathiya.ViewModel
{
    public class BranchViewModel : BaseModelViewModel
    {
        public BranchViewModel()
        {
            branchDetailsList = new List<BranchDetails>();
            branchDetails = new BranchDetails();
        }
        public List<BranchDetails> branchDetailsList { get; set; }
        public BranchDetails branchDetails { get; set; }
        public class BranchDetails
        {
            public int BranchId { get; set; }
            [Required(ErrorMessage = "City Name is required.")]

            public int? CityId { get; set; }
            [Required(ErrorMessage = "State is required.")]

            public int? StateId { get; set; }
            [Required(ErrorMessage = "Country is required.")]

            public int? CountryId { get; set; }

            [Required(ErrorMessage = "Branch Name is required.")]
            [StringLength(100, ErrorMessage = "Branch Name can't be longer than 100 characters.")]
            public string BranchName { get; set; } = null!;

            [Required(ErrorMessage = "Address is required.")]
            [StringLength(200, ErrorMessage = "Address can't be longer than 200 characters.")]
            public string? Address { get; set; }
            public string CityName { get; set; }
            public string StateName { get; set; }
            public string CountryName { get; set; }

            public List<LocCountry> CountryList { get; set; }
            public List<LocCity> CityList { get; set; }
            public List<LocState> StateList { get; set; }
            public int TotalRecords { get; set; }
    
        }
    }
}