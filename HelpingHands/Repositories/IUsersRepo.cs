using Dapper;
using HelpingHands.Models;
using System.Data;

namespace HelpingHands.Repositories
{
    public interface IUsersRepo
    {
        IEnumerable<User> GetUsers(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        User GetUserById(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void UpdateUser(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void DeleteUser(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void UpdatePassword(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void GetUserByRole(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void GetUserByStatus(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);
    }
}
