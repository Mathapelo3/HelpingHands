using Dapper;
using HelpingHands.Data;
using HelpingHands.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HelpingHands.Repositories
{
    public class SuburbRepo : ISuburbsRepo
    {
        private readonly IDbConnection _connection;
        private readonly ApplicationDbContext _context;

        public SuburbRepo(IDbConnection connection, ApplicationDbContext context )
        {
            _connection = connection;
            _context = context;
        }

        public void DMLSuburbs(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            _connection.Execute(procedureName, parameters, commandType: commandType);
        }

        public Suburb GetSuburbByCityId(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.QueryFirstOrDefault<Suburb>(procedureName, parameters, commandType: commandType);
        }

        public IEnumerable<Suburb> GetSuburbs(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.Query<Suburb>(procedureName, parameters, commandType: commandType);
        }

        public IEnumerable<Suburb> GetSuburbsByCity(long cityId)
        {
            var query = "SELECT * FROM Suburbs WHERE CityId = @CityId";

            var suburb = _connection.Query<Suburb>(query, new { CityId = cityId });

            return suburb;
        }

        Task<IEnumerable<SuburbVM>> ISuburbsRepo.GetSuburbs()
        {
            throw new NotImplementedException();
        }
    }
}
