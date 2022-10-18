using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Data
{
    public interface IDatabase
    {
        DataTable Read(string query, SqlParameter[] parameters = null);
        void Write(string sql, List<SqlParameter> parameters);
    }
}
