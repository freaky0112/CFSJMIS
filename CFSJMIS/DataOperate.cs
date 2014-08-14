using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CFSJMIS {
    /// <summary>
    /// 数据操作
    /// </summary>
    public abstract class DataOperate {

        public static bool login(string name, string password) {
            int isLogined = 0;
            StringBuilder sql=new StringBuilder();
            sql.Append("SELECT count(*) from account where name=");
            sql.Append("@name");
            sql.Append(" and password=");
            sql.Append("@password");
            MySqlParameter[] pt=new MySqlParameter[]{
                new MySqlParameter("@name",name),
                new MySqlParameter("password",password)
            };
            string result = MySqlHelper.ExecuteScalar(Common.strConntection(), CommandType.Text, sql.ToString(), pt).ToString();
            isLogined=Int32.Parse(result);
            if (isLogined == 1) {
                return true;
            } else {
                return false;
            }
        }

    }
}
