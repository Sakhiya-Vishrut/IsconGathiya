using IsconGathiya.Common;
using IsconGathiya.Domain.DataModels;
using IsconGathiya.Helper;
using IsconGathiya.Helper.Mapper.BranchMapper;
using IsconGathiya.Service.Branchs;
using IsconGathiya.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace IsconGathiya.Controllers
{
    public class BranchController : BaseController
    {
        private readonly IBranchRepository _branchrepository;
        public BranchController(IBranchRepository branchrepository)
        {
            _branchrepository = branchrepository;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var branchList = new BranchViewModel
                {
                    branchDetailsList = (await _branchrepository.GetBranchDataWithFilter()).ToModel()
                };
                return View(branchList);
            }
            catch (Exception ex)
            {
                return StatusCode(111, "Internal server error");
            }
        }

        [HttpGet, Route("Branch/add", Name = "Branch_Add")]
        [HttpGet, Route("Branch/edit/{encodedBranchId}", Name = "Branch_Edit")]
        public IActionResult BranchForm(string? encodedBranchId)
        {
            try
            {
                var model = new BranchViewModel();
                model.branchDetails = new BranchViewModel.BranchDetails();
                //model.PageTitle = string.IsNullOrEmpty(encodedCategoryId) ? "Category Add" : "Category Edit";

                if (encodedBranchId != null)
                {
                    int? BranchId = encodedBranchId.Decode();
                    model.branchDetails = _branchrepository.DetailBranch(BranchId).ToModel();

                }

                return View(model);
            }
            catch (Exception ex)
            {

                AddSweetAlertErrorPopup(ex.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddeditBranch(BranchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("BranchForm", model);
            }

            try
            {
                var isSuccess = await _branchrepository.AddEditBranch(model.branchDetails.ToModel());
                string successMessage = isSuccess ? "Branch added/edited successfully" : "Something went wrong";

                AddSweetAlertSuccessPopup(successMessage);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AddSweetAlertErrorPopup("An error occurred while saving the branch.");
                return View("BranchForm", model);
            }
        }

    }
}
