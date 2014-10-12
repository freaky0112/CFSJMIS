//
//                       _oo0oo_
//                      o8888888o
//                      88" . "88
//                      (| -_- |)
//                      0\  =  /0
//                    ___/`---'\___
//                  .' \\|     |// '.
//                 / \\|||  :  |||// \
//                / _||||| -:- |||||- \
//               |   | \\\  -  /// |   |
//               | \_|  ''\---/''  |_/ |
//               \  .-\__  '-'  ___/-. /
//             ___'. .'  /--.--\  `. .'___
//          ."" '<  `.___\_<|>_/___.' >' "".
//         | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//         \  \ `_.   \_ __\ /__ _/   .-` /  /
//     =====`-.____`.___ \_____/___.-`___.-'=====
//                       `=---='
//
//
//     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//
//               佛祖保佑         永无BUG
//
//
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using CFSJMIS.Security;

namespace CFSJMIS {
    public abstract class Load {
        public static bool configRead() {
            try {


                XDocument config = XDocument.Load(Common.XMLConfig);
                var cf = from el in config.Descendants("Config")
                         select new {
                             address = el.Element("Address").Value,
                             port = el.Element("Port").Value,
                             uid=el.Element("UID").Value,
                             pwd=el.Element("PWD").Value
                         };
                Security.SymmetricMethod method = new SymmetricMethod();
                foreach (var a in cf) {
                    Common.sshServer = a.address;
                    Common.sshPort = Int32.Parse(a.port);
                    Common.sshUID = method.Decrypto(a.uid);
                    Common.sshPWD = method.Decrypto(a.pwd);
                }
            } catch (Exception ex) {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 读取国土所
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static List<string> gtxRead(string node) {
            //string[] nodes;
            XElement gtx = XElement.Load(Common.XMLTown);
            IEnumerable<XElement> elements =
                from el in gtx.Elements(node)
                //where (string)el.Attribute("name") == node
                select el;
            List<string> gtxs = new List<string>();
            gtxs.Add("请选择所在国土所");
            foreach (XElement el in elements) {
                gtxs.Add((string)el.Attribute("NAME"));
            }
            return gtxs;
        }
        /// <summary>
        /// 读取国土所对应乡镇
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static List<string> townRead(string node) {
            XElement gtx = XElement.Load(Common.XMLTown);
            IEnumerable<XElement> elements =
                from el in gtx.Elements("GTX")
                where (string)el.Attribute("NAME") == Common.table
                select el;
            IEnumerable<XElement> element =
                from el in elements.Elements(node)
                select el;
            List<string> towns = new List<string>();
            towns.Add("请选择导入乡镇");
            foreach (XElement el in element) {
                towns.Add((string)el.Attribute("NAME"));
            }
            return towns;
        }
        public static List<string> ethnicRead() {
            XElement ethnic = XElement.Load(Common.XMLEthnic);
            IEnumerable<XElement> element =
                from el in ethnic.Elements("Ethnic")
                select el;
            List<string> Ethnics = new List<string>();
            foreach (XElement el in element) {
                Ethnics.Add(el.Value);
            }
            return Ethnics;
        }
        /// <summary>
        /// 读取导入文件
        /// </summary>
        /// <returns></returns>
        public static string getPath() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c://";
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "xls文件|*.xls|xlsx文件|*.xlsx|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                return openFileDialog.FileName;
            } else {
                return null;
            }
        }
        /// <summary>
        /// 获取保存目录
        /// </summary>
        /// <returns></returns>
        public static string getSavePath() {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //设置文件类型  
            saveFileDialog.Filter = "xls文件|*.xls|xlsx文件|*.xlsx|所有文件|*.*";
            //保存对话框是否记忆上次打开的目录  
            saveFileDialog.RestoreDirectory = true;
            try {

                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    return saveFileDialog.FileName;
                } else {
                    return null;
                }
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}
