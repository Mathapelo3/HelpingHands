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

        public PatientProfileVM GetPatientById(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.QueryFirstOrDefault<PatientProfileVM>(procedureName, parameters, commandType: commandType);
        }

        public IEnumerable<PatientProfileVM> GetPatients(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.Query<PatientProfileVM>(procedureName,parameters, commandType: commandType);
        }

        public void UpdatePatient(string userId, string surname, string firstName, DateTime dob, string emergencyC, string emergencyP, string info)
        {
            var parameters = new
            {
                UserId = userId,
                Surname = surname,
                FirstName = firstName,
                Dob = dob,
                EmergencyContact = emergencyC,
                EmergencyPerson = emergencyP,
                AdditionalInformation = info

            };

            _connection.Execute("UpdatePatient", parameters, commandType: CommandType.StoredProcedure); ;
        }
    }
}

    

