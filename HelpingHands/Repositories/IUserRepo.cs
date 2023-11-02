using Dapper;
using HelpingHands.Models;
using System.Data;

namespace HelpingHands.Repositories
{
    public interface IUserRepo
    {
        IEnumerable<User> GetUsers(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        Task <User> GetUserByIdAsync(string userId);

        void DMLUser(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);


    }
}
