using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFSJMIS.Biult {
    class GetOffice {
        private static void GetWordVersion(out int wordVerNum, out string wordAppName) {
            //word版本号
            wordVerNum = 0;
            //word应用程序名称
            wordAppName = null;
            string str_KeyName = "Path";
            object objResult = null;
            Microsoft.Win32.RegistryValueKind regValueKind;//指定在注册表中存储值时所用的数据类型，或标识注册表中某个值的数据类型。
            Microsoft.Win32.RegistryKey regKey = null;//表示 Windows 注册表中的项级节点(注册表对象?)
            Microsoft.Win32.RegistryKey regSubKey = null;
            try {
                regKey = Microsoft.Win32.Registry.LocalMachine;//读取HKEY_LOCAL_MACHINE项
                //office2013
                if (regSubKey == null) {
                    regSubKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\15.0", false);
                    wordVerNum = 15;
                    wordAppName = "Word2010";
                    str_KeyName = "Path";
                }
                //office2010
                if (regSubKey == null) {
                    regSubKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\14.0", false);
                    wordVerNum = 14;
                    wordAppName = "Word2010";
                    str_KeyName = "Path";
                }
                //office2007
                if (regSubKey == null) {
                    regSubKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\12.0", false);
                    wordVerNum = 12;
                    wordAppName = "Word2007";
                    str_KeyName = "Path";
                }
                //Office2003
                if (regSubKey == null) {
                    regSubKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\11.0", false);
                    wordVerNum = 11;
                    wordAppName = "Word2003";
                    str_KeyName = "Path";
                }
                // = regSubKey.GetValue(str_KeyName);
                //regValueKind = regSubKey.GetValueKind(str_KeyName);
                //if (regValueKind == Microsoft.Win32.RegistryValueKind.String) {
                //    wordAppName = objResult.ToString();
                //}
            } catch (Exception ex) {
                throw ex;
            } finally {
                if (regKey != null) {
                    regKey.Close();
                    regKey = null;
                }
                if (regSubKey != null) {
                    regSubKey.Close();
                    regSubKey = null;
                }
            }
        }

        public static bool isNotNewOffice() {
            int wordVerNum = 0;
            string wordAppName = string.Empty;
            GetWordVersion(out wordVerNum, out wordAppName);

            if (wordVerNum > 12) {
                return true;
            } else {
                return false;
            }
        }

        public static int getOfficeVersion() {
            int wordVerNum = 0;
            string wordAppName = string.Empty;
            GetWordVersion(out wordVerNum, out wordAppName);
            return wordVerNum;
        }
    }
}
