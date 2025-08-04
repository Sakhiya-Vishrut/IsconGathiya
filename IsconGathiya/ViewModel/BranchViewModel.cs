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

            public int? CityId { get; set; }

            public int? StateId { get; set; }

            [Required(ErrorMessage = "Branch Name is required.")]
            [StringLength(100, ErrorMessage = "Branch Name can't be longer than 100 characters.")]
            public string BranchName { get; set; } = null!;

            [Required(ErrorMessage = "Address is required.")]
            [StringLength(200, ErrorMessage = "Address can't be longer than 200 characters.")]
            public string? Address { get; set; }
        }

    }
}