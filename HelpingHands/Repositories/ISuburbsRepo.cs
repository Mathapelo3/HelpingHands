using Dapper;
using HelpingHands.Models;
using System.Data;

namespace HelpingHands.Repositories
{
    public interface ISuburbsRepo
    {
        IEnumerable<Suburb> GetSuburbs(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        IEnumerable<Suburb>GetSuburbsByCity(long cityId);

        Suburb GetSuburbByCityId(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void DMLSuburbs(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        

    }
}
