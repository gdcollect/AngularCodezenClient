using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularCodezenClient
{
    public static class DB
    {
        private static NgTable GetTable(List<NgTable> tables,string tableName)
        {
            var ng = tables.First(x => x.Name == tableName);
            return ng;
        }
        public static List<NgTable> FetchDBSchema(string cstr)
        {
            List<NgTable> Tables = new List<NgTable>();
            using (SqlConnection conn = new SqlConnection(cstr))
            {
                conn.Open();
                var qrTable = @"SELECT T.name AS Table_Name 
                                FROM   sys.objects AS T  
                                WHERE  T.type_desc = 'USER_TABLE'
                                order by t.name";


                var qr = @"SELECT T.name AS Table_Name ,
                                   C.name AS Column_Name ,
                                   P.name AS Data_Type ,
                                   P.max_length AS Size ,
	                               C.is_nullable as Nullable,
	                               C.is_identity as IdentityV
                            FROM   sys.objects AS T
                                   JOIN sys.columns AS C ON T.object_id = C.object_id
                                   JOIN sys.types AS P ON C.system_type_id = P.system_type_id
                            WHERE  T.type_desc = 'USER_TABLE'
                            order by t.name,c.column_id";

                var primaryQr = @"SELECT A.TABLE_NAME, A.CONSTRAINT_NAME, B.COLUMN_NAME
                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS A, INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE B
                WHERE CONSTRAINT_TYPE = 'PRIMARY KEY' AND A.CONSTRAINT_NAME = B.CONSTRAINT_NAME
                ORDER BY A.TABLE_NAME";

                var cmd = new SqlCommand(qrTable);
                cmd.Connection = conn;
                var rdr = cmd.ExecuteReader();
                var tName = "";
                NgTable tbl = new NgTable();
                while (rdr.Read())
                {
                    tName = rdr["Table_Name"].ToString().Trim();
                    tbl = new NgTable();
                    tbl.Name = tName;
                    if (tbl.Name != null && tbl.Name != "")
                    {
                        Tables.Add(tbl);
                    }
                }
                rdr.Close();

                cmd = new SqlCommand(qr);
                cmd.Connection = conn;
                var rdrFields = cmd.ExecuteReader();
                while (rdrFields.Read())
                {
                    tName = rdrFields["Table_Name"].ToString().Trim();
                    tbl = GetTable(Tables, tName);
                    NgColumn fld = new NgColumn();
                    fld.Name = rdrFields["Column_Name"].ToString().Trim();
                    fld.DataType = rdrFields["Data_Type"].ToString().Trim();
                    fld.Length = rdrFields["Size"].ToString().Trim();
                    if (rdrFields["IdentityV"].ToString().Trim().ToLower() == "true")
                    {
                        fld.IsAuto = true;
                    }
                    else
                    {
                        fld.IsAuto = false;
                    }
                    
                    tbl.Columns.Add(fld);
                }
                rdrFields.Close();

                cmd = new SqlCommand(primaryQr);
                cmd.Connection = conn;
                var rdrPrFields = cmd.ExecuteReader();

                while (rdrPrFields.Read())
                {
                    tName = rdrPrFields["Table_Name"].ToString().Trim().ToLower();
                    var fldName= rdrPrFields["COLUMN_NAME"].ToString().Trim().ToLower();

                    foreach (var t in Tables)
                    {
                        if (t.Name.Trim().ToLower() == tName)
                        {
                            foreach (var f in t.Columns)
                            {
                                if (f.Name.ToLower().Trim() == fldName)
                                {
                                    f.IsPrimary = true;
                                }
                            }
                        }
                    }
                }
                rdrPrFields.Close();

            }

            return Tables;
        }
    }
}
