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
using CFSJMIS.Collections;

namespace CFSJMIS.Biult {
    class CLJD {
        /// <summary>
        /// 初始化应用对象
        /// </summary>
        private static string pText = "";// 文本信息   
        private static int pFontSize = 24;//字体大小  
        private static string pFontName = "宋体";
        private static Microsoft.Office.Interop.Word.WdColor pFontColor = Microsoft.Office.Interop.Word.WdColor.wdColorBlack;//字体颜色 
        private static Microsoft.Office.Interop.Word.WdUnderline pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;//下划线
        private static int pFontBold = 0;//字体粗体  
        private static Microsoft.Office.Interop.Word.WdParagraphAlignment ptextAlignment = 0;//方向 

        private static EcanNum ecanNum = new EcanNum();//数字大写转换
        /// <summary>
        /// 输入处罚决定书
        /// </summary>
        /// <param name="brf"></param>
        public static  void biult(BiultReportForm brf, Data data) {

            brf.NewPage();
            addTitle(brf, data);
            addText(brf, data);
        }

        /// <summary>
        /// 输入并插入下一行
        /// </summary>
        private static void addLine(BiultReportForm brf) {
            brf.InsertText(pText, pFontSize, pFontName, pFontColor, pFontUnderline, pFontBold, ptextAlignment);
            brf.NewLine();
        }
        /// <summary>
        /// 插入文字
        /// </summary>
        /// <param name="brf"></param>
        private static void addTxt(BiultReportForm brf) {
            brf.InsertText(pText, pFontSize, pFontName, pFontColor, pFontUnderline, pFontBold, ptextAlignment);
        }
        /// <summary>
        /// 标题
        /// </summary>
        /// <param name="brf"></param>    
        private static void addTitle(BiultReportForm brf, Data data) {
            if (GetOffice.isNotNewOffice()) {
                brf.TypeBackspace();
            }
            pText = "青田县国土资源局";
            pFontSize = 24;
            pFontName = "宋体";
            ptextAlignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            pFontBold = 1;//设置粗体
            addLine(brf);
            pFontSize = 24;
            pText = "关于对被没收房屋的处理决定";
            addLine(brf);
            pFontSize = 12;
            pFontBold = 0;//设置细体
            pText = "";
            addLine(brf);
            pText = "青土资没作字〔2014〕" + data.Code + String.Format("{0:0000}", data.ConfiscateID) + "号";
            ptextAlignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
            addLine(brf);
        }
        /// <summary>
        /// 输入正文
        /// </summary>
        /// <param name="brf"></param>
        /// <param name="data"></param>
        private static void addText(BiultReportForm brf, Data data) {
            pFontName = "仿宋_GB2312";
            pText = "";
            addLine(brf);
            foreach (string name in data.Names) {
                pText += name + "、";
            }
            pText = pText.TrimEnd('、');
            pText += ":";
            pFontSize = 16;
            ptextAlignment = 0;
            addLine(brf);
            pText = "    我局行政处罚决定书（青土资罚〔2014〕";
            pText += data.Code + String.Format("{0:0000}", data.ID);
            pText += "号）依法没收你户";
            pText += data.BuildDate.ToString().Substring(0, 4);
            pText += "年";
            pText += data.BuildDate.ToString().Substring(4, 2);
            pText += "月在青田县";
            pText += data.Town;
            pText += data.Location;
            if (data.BuildDate >= 201304 && !data.Control.Equals("四级")) {
                pText += "非法建造的房屋。";
            } else {
                pText += "超土地审批限额建造的房屋。";
            }
            addLine(brf);
            pText = "    没收建筑面积计";
            pText += data.ConfiscateArea;
            pText += "平方米（占地";
            if (data.BuildDate >= 201304 && !data.Control.Equals("四级")) {
                pText += data.IllegaArea;
            } else {
                pText += data.ConfiscateFloorArea;
            }
            pText += "平方米，依照《青田县人民政府关于印发青田县实施〈浙江省违法建筑处置规定〉细则（暂行）》（青政发〔2014〕62号）";
            if (data.BuildDate >= 201304) {
                pText += "、《青田县人民政府关于印发青田县实施浙江省违法建筑处置规定细则（暂行）的补充意见的通知》（青政发〔2014〕101号）";
            }
            pText+="有关规定，没收金额为";
            pText += data.ConfiscateAreaUnit;
            pText += "元/平方米，共计人民币";
            pText += ecanNum.CmycurD(data.ConfiscateAreaPrice.ToString()) + "（￥" + Math.Round(data.ConfiscateAreaPrice, 2) + ")。现根据你户申请，经研究决定，由你户购回。";
            pFontUnderline = Microsoft.Office.Interop.Word.WdUnderline.wdUnderlineNone;
            addLine(brf);
            pText = "    现责令你户办理有关手续，否则将另行处理。";
            addLine(brf);
            pText = "";
            addLine(brf);
            addLine(brf);
            pText = "                              2014年   月   日";
            addLine(brf);
        }
    }
}
