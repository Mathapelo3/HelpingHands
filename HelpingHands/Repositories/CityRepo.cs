using Dapper;
using HelpingHands.Models;
using System.Data;

namespace HelpingHands.Repositories
{
    public class CityRepo : ICityRepo
    {
        private readonly IDbConnection _connection;

        public CityRepo(IDbConnection connection)
        {
            _connection = connection;
        }

        public void DMLCities(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            _connection.Execute(procedureName, parameters, commandType: commandType);
        }

        public IEnumerable<City> GetCities(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.Query<City>(procedureName, parameters, commandType: commandType);
        }

        public City GetCityById(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.QueryFirstOrDefault<City>(procedureName, parameters, commandType: commandType);
        }
    }
}
