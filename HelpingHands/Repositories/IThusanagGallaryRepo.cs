using Dapper;
using HelpingHands.Models;
using System.Data;

namespace HelpingHands.Repositories
{
    public interface IThusanagGallaryRepo
    {
        IEnumerable<ThusanagGallery> GetImages(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void InsertImage(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void UpdateImage(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);

        void DeleteImage(string procedureName, DynamicParameters parameters, CommandType commandType = CommandType.StoredProcedure);


    }
}
