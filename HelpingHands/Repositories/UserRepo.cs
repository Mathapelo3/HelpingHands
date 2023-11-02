using Dapper;
using HelpingHands.Data;
using HelpingHands.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HelpingHands.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly IDbConnection _connection;
        private readonly ApplicationDbContext _context;

        public UserRepo(IDbConnection connection, ApplicationDbContext application)
        {
            _connection = connection;
            _context = application;
        }

        public void DMLUser(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            _connection.Execute(procedureName, parameters, commandType: commandType);
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            var user = await _context.AspNetUsers.FirstOrDefaultAsync(x => x.Id == userId);
            return user;
        }


        public IEnumerable<User> GetUsers(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure)
        {
            return _connection.Query<User>(procedureName, parameters, commandType: commandType);
        }

    }
}
