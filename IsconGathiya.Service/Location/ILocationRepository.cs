using IsconGathiya.Domain.DataModels;

namespace IsconGathiya.Service.Location
{
    public interface ILocationRepository
    {
        List<LocCountry> GetCountryList();
        Task<List<LocState>> GetStateByCountryId(int CountryID);
        Task<List<LocCity>> GetCitiesByState(int stateId);
    }
}
