using IsconGathiya.Common;
using IsconGathiya.Domain.DataModels;
using IsconGathiya.Helper;
using IsconGathiya.Helper.Mapper.BranchMapper;
using IsconGathiya.Service.Branchs;
using IsconGathiya.Service.Location;
using IsconGathiya.ViewModel;
using IsconGathiya.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IsconGathiya.Controllers
{
    public class BranchController : BaseController
    {
        private readonly IBranchRepository _branchrepository;
        private readonly ILocationRepository _locationrepository;
        public BranchController(IBranchRepository branchrepository,ILocationRepository locationRepository)
        {
            _branchrepository = branchrepository;
            _locationrepository = locationRepository;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var branchList = new BranchViewModel();
                branchList.branchDetailsList = (await _branchrepository.GetBranchDataWithFilter()).ToModel();
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
                //model.PageTitle = string.IsNullOrEmpty(encodedBranchId) ? "Branch Add" : "Branch Edit";


                if (encodedBranchId != null)
                {
                    int? BranchId = encodedBranchId.Decode();
                    model.branchDetails = _branchrepository.DetailBranch(BranchId).ToModel();
                }
                model.branchDetails.CountryList = _locationrepository.GetCountryList();

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

            try
            {
                var isSuccess = await _branchrepository.AddEditBranch(model.branchDetails.ToModel());
                
                if(model.branchDetails.BranchId == 0)
                {
                    AddSweetAlertSuccessPopup(ConstantMessage.Branch);

                } else
                {
                    AddSweetAlertSuccessPopup(ConstantMessage.BranchEditSuccessful);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                AddSweetAlertErrorPopup("An error occurred while saving the branch.");
                return View("BranchForm", model);
            }
        }
        #region BranchDelete
        [HttpPost, Route("Branch/delete", Name = "BranchDelete")]
        public async Task<IActionResult> BranchDelete(string encodedBranchlId)
        {
            try
            {
                if (!string.IsNullOrEmpty(encodedBranchlId))
                {
                    int? branchId = encodedBranchlId.Decode();
                    bool isDelete = await _branchrepository.DeleteBranch(branchId);


                    if (isDelete)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToRoute("Error_404");
            }
        }
        #endregion
    }
}
