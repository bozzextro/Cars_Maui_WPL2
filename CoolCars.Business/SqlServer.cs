using System;
using System.Data;
using System.Data.SqlClient;

namespace CoolCars.Business
{
    public class SqlServer
    {
        protected SqlConnection _connection;
        protected SqlCommand _command;
        protected SqlDataAdapter _adapter;

        public SqlServer()
        {
            try
            {
                _connection = new SqlConnection(LocalSettings.ConnectionString);
                _command = new SqlCommand();
                _command.Connection = _connection;
                _adapter = new SqlDataAdapter(_command);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected BaseResult ExecuteNonQuery(string query, CommandType commandType = CommandType.Text)
        {
            BaseResult result = new BaseResult();
            try
            {
                _command.CommandText = query;
                _command.CommandType = commandType;
                _connection.Open();
                _command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return result;
        }

        protected SelectResult ExecuteReader(string query, CommandType commandType = CommandType.Text)
        {
            SelectResult result = new SelectResult();
            try
            {
                _command.CommandText = query;
                _command.CommandType = commandType;
                _connection.Open();
                
                // Use the adapter to fill the DataTable
                _adapter.Fill(result.Data);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Exception = ex;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return result;
        }
    }
}
