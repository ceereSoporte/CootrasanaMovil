using System;
using System.Collections.Generic;
using System.Text;

namespace Cootrasana.Models
{
    public interface ConectionSQLite
    {
        SQLite.SQLiteConnection GetConnection();
    }
}
