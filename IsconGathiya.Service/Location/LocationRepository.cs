using IsconGathiya.Common.DependencyInjection;
using IsconGathiya.Domain.DataContext;
using IsconGathiya.Domain.DataModels;
using Microsoft.EntityFrameworkCore;

namespace IsconGathiya.Service.Location
{
    [TransientDependency(ServiceType = typeof(ILocationRepository))]
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;
        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #region Cascading of Conutry , State and City

        #region GetCountryList
        public List<LocCountry> GetCountryList()
        {
            var CountryList = (from locCountry in _context.LocCountries
                               orderby locCountry.Name ascending
                               select new LocCountry
                               {
                                   Id = locCountry.Id,
                                   Name = locCountry.Name
                               }).ToList();

            return CountryList;
        }

        #endregion

        #region GetStateByCountryId
        public async Task<List<LocState>> GetStateByCountryId(int CountryId)
        {
            var stateList = await (from locState in _context.LocStates
                                   where locState.CountryId == CountryId
                                   orderby locState.Name ascending
                                   select new LocState
                                   {
                                       Id = locState.Id,
                                       Name = locState.Name
                                   }).ToListAsync();

            return stateList;
        }
        #endregion

        #region GetCitiesByState
        public async Task<List<LocCity>> GetCitiesByState(int stateId)
        {
            var cityList = await (from locCity in _context.LocCities
                                  where locCity.StateId == stateId
                                  orderby locCity.Name ascending
                                  select new LocCity
                                  {
                                      Id = locCity.Id,
                                      Name = locCity.Name
                                  }).ToListAsync();

            return cityList;
        }
        #endregion
        #endregion
    }
}
