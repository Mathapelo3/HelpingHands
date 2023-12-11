using Dapper;
using HelpingHands.Models;
using System.Data;
using System.Globalization;

namespace HelpingHands.Repositories
{
    public interface IPatientRepo
    {
        IEnumerable<PatientProfileVM> GetPatients(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        PatientProfileVM GetPatientById(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);  

        void  DMLPatient(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void UpdatePatient(string userId, string surname, string firstName, DateTime dob, string emergencyC, string emergencyP, string info);

    }
}
