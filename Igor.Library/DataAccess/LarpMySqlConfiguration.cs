using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Igor.Library.DataAccess
{
    public class LarpMySqlConfiguration : DbConfiguration
    {
        public LarpMySqlConfiguration()
        {
            SetHistoryContext(
                "MySql.Data.MySqlClient", (conn, schema) => new LarpMySqlHistoryContext(conn, schema));
        }
    }
}
