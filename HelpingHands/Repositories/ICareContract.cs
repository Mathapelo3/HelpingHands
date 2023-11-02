using Dapper;
using HelpingHands.Models;
using System.Data;

namespace HelpingHands.Repositories
{
    public interface ICareContract
    {
        IEnumerable<CareContract> GetContracts(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        CareContract GetContractById(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void CreateCareContract(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void UpdateContract(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void DeleteContract(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void GetPatientContract(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void GetContractByStatus(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

    }
}
