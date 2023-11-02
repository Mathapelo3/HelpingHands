using Dapper;
using HelpingHands.Models;
using System.Data;

namespace HelpingHands.Repositories
{
    public interface IPatientRepo
    {
        IEnumerable<Patient> GetPatients(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        Patient GetPatientById(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);  

        void  DMLPatient(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);
    }
}
