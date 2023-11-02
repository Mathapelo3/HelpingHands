using Dapper;
using HelpingHands.Models;
using System.Data;

namespace HelpingHands.Repositories
{
    public interface ICityRepo
    {
        IEnumerable<City> GetCities(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        City GetCityById(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void DMLCities(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

       
    }
}
