using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WalterChavarry.Repository
{
    public class Conection
    {
        public SqlConnection GetConection()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
            return cn;
        }
    }
}