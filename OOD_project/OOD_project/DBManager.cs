using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_project
{
    internal class DBManager
    {
        private DBManager() { }

        public static DBManager Instance { get {  return DBManager.Instance; } }

        public DbConnection Connection { get; set; }
        public DbCommand Command { get; set; }

        public DbDataReader Reader { get; set; }
    }
}
