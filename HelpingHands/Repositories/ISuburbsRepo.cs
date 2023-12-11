using Dapper;
using HelpingHands.Models;
using System.Data;

namespace HelpingHands.Repositories
{
    public interface ISuburbsRepo
    {
        Task<IEnumerable<SuburbVM>>GetSuburbs();

        IEnumerable<Suburb>GetSuburbsByCity(long cityId);

        Suburb GetSuburbByCityId(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void DMLSuburbs(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        

    }
}
