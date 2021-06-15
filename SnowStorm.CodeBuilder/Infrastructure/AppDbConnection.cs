using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using static SnowStorm.CodeBuilder.Infrastructure.References;

namespace SnowStorm.CodeBuilder.Infrastructure
{

    public interface IAppDbConnection : IDisposable
    {
        string ConnectionString { get; set; }
        SqlConnection DbConnection { get; }
        void CloseConnection();
        string SafeString(ref SqlDataReader reader, ColIndex index);
        int? SafeInt(ref SqlDataReader reader, ColIndex index);
        SqlDataReader Read(string commandText, List<SqlParameter> parameters = null);
    }

    public class AppDbConnection: IAppDbConnection, IDisposable
    {
        public AppDbConnection() { }

        public string ConnectionString { get; set; }

        private SqlConnection _dbConnection = null;
        public SqlConnection DbConnection
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ConnectionString))
                    throw new Exception("Missing Connection String!!!");

                if (_dbConnection == null || _dbConnection.ConnectionString != ConnectionString)
                {
                    CloseConnection();
                    _dbConnection = new SqlConnection(ConnectionString);
                    _dbConnection.Open();
                }
                return _dbConnection;
            }
        }

        public void CloseConnection()
        {
            if (!string.IsNullOrWhiteSpace(ConnectionString) && _dbConnection != null && _dbConnection.State != System.Data.ConnectionState.Closed)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
                _dbConnection = null;
            }
        }

        public string SafeString(ref SqlDataReader reader, ColIndex index)
        {
            string value = reader[(int)index] == DBNull.Value ? null : reader.GetString((int)index);
            return value;
        }

        public int? SafeInt(ref SqlDataReader reader, ColIndex index)
        {
            int? value = null;
            if (reader[(int)index] != DBNull.Value)
                value = Convert.ToInt32(reader[(int)index]);
            return value;
        }

        public SqlDataReader Read(string commandText, List<SqlParameter> parameters = null)
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new Exception("No connections string defined!");

            if (string.IsNullOrWhiteSpace(commandText))
                throw new Exception("No commandText specified!");


            var cmd = new SqlCommand
            {
                Connection = new SqlConnection(ConnectionString),
                CommandText = commandText
            };
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }
            cmd.Connection.Open();
            var reader = cmd.ExecuteReader();
            return reader;
        }

        public void Dispose() 
        {
            CloseConnection();
        }

    }

}
