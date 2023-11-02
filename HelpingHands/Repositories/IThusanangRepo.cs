using Dapper;
using HelpingHands.Models;
using System.Data;

namespace HelpingHands.Repositories
{
    public interface IThusanangRepo
    {
        IEnumerable<Thusanang> GetDetails(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void CreateDetails(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void UpdateDetails(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

       


    }
}
