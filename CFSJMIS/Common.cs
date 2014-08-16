﻿using System;
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
        /// <summary>
        /// 判断是否为数字
        /// </summary>
        /// <param name="strNumber">数字</param>
        /// <returns></returns>
        public static bool IsNumber(string strNumber) {
            //看要用哪種規則判斷，自行修改strValue即可

            //strValue = @"^\d+[.]?\d*$";//非負數字
            string strValue = @"^\d+(\.)?\d*$";//數字
            //strValue = @"^\d+$";//非負整數
            //strValue = @"^-?\d+$";//整數
            //strValue = @"^-[0-9]*[1-9][0-9]*$";//負整數
            //strValue = @"^[0-9]*[1-9][0-9]*$";//正整數
            //strValue = @"^((-\d+)|(0+))$";//非正整數

            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(strValue);
            return r.IsMatch(strNumber);
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

        public static string table = "";

        public static string tableConfiscate() {
            return table + "没收";
        }
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

        #region XML信息
        public const string XMLTOWN = "XMLTowns.xml";

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
    /// <summary>
    /// 没收计算
    /// </summary>
    public abstract class ConfiscateCalculate {

        public static Data getConfiscateData(Data data) {

            if (isNotConfiscate(data)) {
                data.QuotaArea = getQuotaArea(data);//没收限额面积
                data.ConfiscateFloorArea = getConfiscateFloorArea(data);//没收占地面积
                data.ConfiscateArea = getConfiscateArea(data);//没收面积
                data.ConfiscateAreaUnit = getConfiscateAreaUnit(data);//没收单价
                data.ConfiscateAreaPrice = getConfiscateAreaPrice(data);//没收金额
            } else {
                data.ConfiscateFloorArea = 0;//没收占地面积
                data.ConfiscateArea = 0;//没收面积
                data.ConfiscateAreaUnit = 0;//没收单价
                data.ConfiscateAreaPrice = 0;//没收金额
            }

            return data;
        }
        /// <summary>
        /// 获取没收金额
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static double getConfiscateAreaPrice(Data data) {
            double confiscateAreaPrice = data.ConfiscateArea * data.ConfiscateAreaUnit;
            return confiscateAreaPrice;
        }

        /// <summary>
        /// 是否没收
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool isNotConfiscate(Data data) {
            string timeandowner = "";
            if (data.BuildDate < 198700) {
                return false;
            } else if (data.BuildDate < 199900) {
                timeandowner = "99年以前" + data.LandOwner;
            } else if (data.BuildDate < 201304) {
                timeandowner = "99年以后" + data.LandOwner;
            }

            switch (timeandowner) {
                case "99年以前国有":
                    return getConfiscateFloorArea(data) > 20;
                case "99年以后国有":
                    return getConfiscateFloorArea(data) > 10;
                case "99年以前集体":
                    return getConfiscateFloorArea(data) > 45;
                case "99年以后集体":
                    return getConfiscateFloorArea(data) > 20;
                default:
                    return false;
            }
        }
        /// <summary>
        /// 获取没收面积
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static double getConfiscateArea(Data data) {
            double confiscateArea = new double();
            if (data.Layer <= 7) {
                //高度小于7层时
                confiscateArea = data.ConfiscateFloorArea * data.Layer;
            } else {
                confiscateArea = data.ConfiscateFloorArea * 7;
            }
            return confiscateArea;
        }


        /// <summary>
        /// 或取超限额占地面积
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static double getConfiscateFloorArea(Data data) {
            double confiscateFloorArea = data.Area - getBigNumber(data.LegalArea, getQuotaArea(data));
            return confiscateFloorArea;
        }
        /// <summary>
        /// 获取审批限额
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static int getQuotaArea(Data data) {
            int quotaArea = 0;
            foreach (string account in data.Accounts.Split('+')) {
                int member = 0;
                if (!string.IsNullOrEmpty(account)) {
                    member = Int32.Parse(account);
                }
                if (data.Control.Equals("四级")) {
                    if (member <= 3) {//四级控制区人数1-3为90平方米
                        quotaArea += 90;
                    } else if (member > 5) {//四级控制区人数1-3为120平方米
                        quotaArea += 120;
                    } else {//四级控制区人数4-5为90平方米
                        quotaArea += 100;
                    }
                } else {//其余均为90平方米
                    quotaArea += 90;
                }
            }
            return quotaArea;

        }
        /// <summary>
        /// 获取没收单价
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static int getConfiscateAreaUnit(Data data) {
            int ConfiscateAreaUnit = 0;
            int[,] price = { { 800, 1200, 600, 900, 400, 600, 200, 300, 80, 120, 40, 60, 0, 0 }, { 1200, 1800, 800, 1200, 600, 900, 400, 600, 150, 225, 70, 105, 40, 60 }, { 1500, 2250, 1000, 1500, 800, 1200, 600, 900, 200, 300, 100, 150, 50, 75 } };
            string orign = data.Control + data.LandOwner;
            int a = 0;
            int b = 12;
            if (data.BuildDate < 199900) {
                b = 0;
            } else if (data.BuildDate < 201010) {
                b = 1;
            } else {
                b = 2;
            }

            if (data.Control.Equals("一级I类")) {
                a = 0;
            } else if (data.Control.Equals("一级II类")) {
                a = 2;
            } else if (data.Control.Equals("一级III类")) {
                a = 4;
            } else if (data.Control.Equals("二级")) {
                a = 6;
            } else if (data.Control.Equals("三级非集镇")) {
                a = 8;
            } else if (data.Control.Equals("三级集镇")) {
                a = 10;
            } else {
                a = 12;
            }
            if (data.LandOwner.Equals("国有")) {
                a = a + 1;
            }
            ConfiscateAreaUnit = price[b, a];
            return ConfiscateAreaUnit;

        }

        /// <summary>
        /// 比较a,b两数大小返回大值
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static double getBigNumber(double a, double b) {
            if (a > b) {
                return a;
            } else {
                return b;
            }
        }
    }
}
