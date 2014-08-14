using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CFSJMIS {
    public class Common {
        /// <summary>
        /// 是否为IP
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <returns></returns>
        public static bool isIP(string ipAddress) {
            Regex reg = new Regex(@"(?<ip>(((\d{1,2})|(1\d{2,2})|(2[0-4][0-9])|(25[0-5]))\.){3,3}((\d{1,2})|(1\d{2,2})|(2[0-4][0-9])|(25[0-5])))");
            return reg.IsMatch(ipAddress);
        }
        /// <summary>
        /// 是否为端口号
        /// </summary>
        /// <param name="port">端口号</param>
        /// <returns></returns>
        public static bool isPort(string port) {
            Regex reg = new Regex(@"^([0-9]|[1-9]\d|[1-9]\d{2}|[1-9]\d{3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6553[0-5])$");
            return reg.IsMatch(port);
        }

        #region ssh连接信息
        public static string sshServer = "192.168.1.3";
        public static int sshPort = 2222;
        public static string sshUID = "root";
        public static string sshPWD = "admin";

        public static Boolean sshConnected = false;
        #endregion


        #region mysql数据库链接
        private const string SERVER = "localhost";

        private const uint PORT = 3306;

        private const string DATABASE = "CFSJ";

        private const string UID = "root";

        private const string PWD = "admin";

        private const string CHARSET = "'utf8'";
        /// <summary>
        /// 返回数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string strConntection() {
            StringBuilder conn = new StringBuilder();
            conn.Append("server=");
            conn.Append(SERVER);
            conn.Append(";port=");
            conn.Append(PORT);
            conn.Append(";database=");
            conn.Append(DATABASE);
            conn.Append(";uid=");
            conn.Append(UID);
            conn.Append(";pwd=");
            conn.Append(PWD);
            conn.Append(";charset=");
            conn.Append(CHARSET);
            return conn.ToString();

        }
        #endregion
    }

    public class Messages {
        public const string SSHCONNECTED = "服务器连接成功";

        public const string SERVER_ERROR = "请输入有效服务器地址";

        public const string PORT_ERROR = "请输入有效端口";

        public const string LOGIN_SUCCESS = "登录成功";

        public const string LOGIN_ERROR = "登录失败,请检查密码";

        public const string SSH_ERROR = "请连接服务器";

        public const string EXIT = "确认退出";
    }
}
