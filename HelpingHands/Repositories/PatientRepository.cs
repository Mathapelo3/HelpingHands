using Dapper;
using HelpingHands.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HelpingHands.Repositories
{
    public class PatientRepository : IPatientRepo
    {
        private readonly IDbConnection _connection;

        public PatientRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void DMLPatient(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            _connection.Execute(procedureName, parameters, commandType: commandType);
        }

        public Patient GetPatientById(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.QueryFirstOrDefault<Patient>(procedureName, parameters, commandType: commandType);
        }

        public IEnumerable<Patient> GetPatients(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.Query<Patient>(procedureName,parameters, commandType: commandType);
        }

        //public void CreatePatient(string userId, string surname, string firstName,string gender, DateOnly DoB, string suburbId, string emergencyP, string emergencyC, string info)
        //{
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@Surname",surname );
        //    parameters.Add("@FirstName",firstName);
        //    parameters.Add("@gender", gender);
        //    parameters.Add("@DoB", DoB);
        //    parameters.Add("@Suburb", suburbId);
        //    parameters.Add("@EmergencyP", emergencyP);
        //    parameters.Add("@EmergencyC", emergencyC);
        //    parameters.Add("@Info", info);
        //    parameters.Add("@Id", userId);
        //    _connection.Execute("CreatePatient", parameters, commandType: CommandType.StoredProcedure);

        //}
    }
}

    

