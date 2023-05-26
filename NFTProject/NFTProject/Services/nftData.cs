using NFTProject.Models;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using MySqlConnector;

namespace NFTProject.Services
{
    public class nftData : InftData
    {
        public IConfiguration _configuration { get; }
        public string _connectionString { get; }

        public nftData(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<bool> AddNewNFT(NFTData data)
        {
            var parameters = new DynamicParameters();
            parameters.Add("tokenID", data.tokenId, DbType.String);
            parameters.Add("nftName", data.NFTName, DbType.String);
            parameters.Add("nftDescription", data.NFTDescription, DbType.String);
            parameters.Add("nftUrl", data.NFTImageURL, DbType.String);
            parameters.Add("nfthash", data.NFTImageHash, DbType.String);

            using (var conn = new MySqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                        await conn.ExecuteAsync("AddNFT", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return true;
        }

        public async Task<IEnumerable<NFTData>> GetNFTDataByToken(uint id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("tokenID", id, DbType.Int32);
            IEnumerable<NFTData> NFTEntries;
            using (var conn = new MySqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    NFTEntries = await conn.QueryAsync<NFTData>("GetNFTDetails", parameters,commandType: CommandType.StoredProcedure);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            return NFTEntries;
        }
    }
    
}
