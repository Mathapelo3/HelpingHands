using Dapper;
using HelpingHands.Models;
using System.Data;

namespace HelpingHands.Repositories
{
    public class ThusanangGallaryRepo : IThusanagGallaryRepo
    {
        private readonly IDbConnection _connection;

        public ThusanangGallaryRepo(IDbConnection connection)
        {
            _connection = connection;
        }

        public void DeleteImage(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ThusanagGallery> GetImages(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            throw new NotImplementedException();
        }

        public void InsertImage(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            throw new NotImplementedException();
        }

        public void UpdateImage(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            throw new NotImplementedException();
        }
    }
}
