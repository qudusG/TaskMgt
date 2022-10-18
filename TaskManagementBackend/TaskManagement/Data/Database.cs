using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Data
{
    public class Database : IDatabase
    {
        private readonly AppContext _context;
        private readonly string connectionString;
        public Database(AppContext context, IConfiguration configuration)
        {
            _context = context;
            connectionString = configuration.GetConnectionString("TaskMgt");
        }
        public DataTable Read(string query, SqlParameter[] parameters = null)
        {
            var dataTable = new DataTable();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using var command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                SqlDataReader reader = command.ExecuteReader();
                dataTable.Load(reader);
                reader.Close();
                connection.Close();
            }
            return dataTable;
        }
        public void Write(string sql, List<SqlParameter> parameters)
        {
            _context.Database.ExecuteSqlRaw(sql, parameters);
        }
        
    }
}
