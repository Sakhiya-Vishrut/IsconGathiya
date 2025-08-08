using IsconGathiya.Common;
using IsconGathiya.Service.Location;
using IsconGathiya.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IsconGathiya.Controllers
{
    public class GeneralController : BaseController
    {
        private readonly ILocationRepository _locationrepository;
        public GeneralController(ILocationRepository locationRepository)
        {
            _locationrepository = locationRepository;
        }
        public IActionResult Index()
        {

            return View();
        }

        #region GetStateByCountry
        [HttpGet]
        public async Task<IActionResult> GetStateByCountry(int countryId)
        {
            var states = await _locationrepository.GetStateByCountryId(countryId);

            var jsonResult = states.Select(x => new
            {
                Value = x.Id,
                Text = x.Name
            }).ToList();
            var statusCode = 200; // Or your own custom status code
            var message = "States fetched successfully";

            return Json(JsonResultData.SetJsonModel(statusCode, message, jsonResult));
        }
        #endregion

        #region GetCitiesByState
        [HttpGet]
        public async Task<IActionResult> GetCitiesByState(int stateId)
        {
            var cities = await _locationrepository.GetCitiesByState(stateId);

            var jsonResult = cities.Select(x => new
            {
                Value = x.Id,
                Text = x.Name
            }).ToList();

            var statusCode = 200; // Or your own custom status code
            var message = "States fetched successfully";

            return Json(JsonResultData.SetJsonModel(statusCode, message, jsonResult));
        }
        #endregion
    }
}
