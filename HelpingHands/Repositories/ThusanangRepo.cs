using Dapper;
using HelpingHands.Models;
using System.Data;

namespace HelpingHands.Repositories
{
    public class ThusanangRepo : IThusanangRepo
    {
        private readonly IDbConnection _connection;

        public ThusanangRepo(IDbConnection connection)
        {
            _connection = connection;
        }

        public void CreateDetails(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Thusanang> GetDetails(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            throw new NotImplementedException();
        }

        public void UpdateDetails(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            throw new NotImplementedException();
        }
    }
}
